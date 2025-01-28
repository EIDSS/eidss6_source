using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using bv.common.Configuration;
using bv.common.db.Core;
using DevExpress.Web;
using EIDSS;
using eidss.avr.db.CacheReceiver;
using eidss.avr.db.Common;
using eidss.model.Avr.Commands.Layout;
using eidss.model.Avr.View;
using eidss.avr.mweb.Utils;
using Microsoft.SqlServer.Types;
using System.Data.SqlClient;
using System.Web;
using eidss.web.common.Utils;
using DotSpatial.Projections;
using GeospatialServices.GeoJSON;
using eidss.gis;
using eidss.gis.common;
using eidss.model.Core;


namespace eidss.avr.mweb.Controllers
{
    public class MapController : Controller
    {

        public ActionResult AvrSymbol(string id)
        {
            long l_id = long.Parse(id);
            byte[] image = gis.GisInterface.GetSymbolImage(l_id);
            if (image.Length > 0) {return File(image, "image/png");}
            return null;
        }

        public ActionResult Index(long layoutId)
        {
            var connectionCredentials = new bv.common.Configuration.ConnectionCredentials();
            string connection = connectionCredentials.ConnectionString;

            AvrServiceAccessability access = AvrServiceAccessability.Check();
            if (!access.IsOk)
            {
                return View("AvrServiceError", (object)access.ErrorMessage);
            }

            return ObjectStorage.Using<AvrPivotViewModel, ActionResult>(viewModel =>
            {
                ViewBag.Title = string.Format(Translator.GetMessageString("webMapTitle"), viewModel.ViewHeader.LayoutName);

                // have we anything selected in combo admin unit? 
                if (!string.IsNullOrEmpty(viewModel.ViewHeader.MapAdminUnitViewColumn))
                {
                    DataSet dataSet;
                    string error = ChartMapHelper.TryToPrepareMapData(viewModel, out dataSet);
                    if (error.Length > 0)
                        return View("AvrServiceError", (object)error);

                    //TEST
                    for (int j = 0; j < dataSet.Tables[1].Rows.Count; j++)
                    {
                        string c_name = dataSet.Tables[1].Rows[j]["ColumnName"].ToString();
                        string c_new_name = dataSet.Tables[1].Rows[j]["ColumnDescription"].ToString();
                        c_new_name = c_new_name.TrimStart(); // Spaces in column name, comes outside
                        dataSet.Tables[0].Columns[c_name].ColumnName = c_new_name;
                    }

                    SharpMap.Rendering.Thematics.ITheme gradLayerTheme;
                    SharpMap.Rendering.Thematics.ITheme chartLayerTheme;
                    string gradLayerName, chartLayerName;
                    gis.GisInterface.GetAvrStyles(layoutId, out gradLayerName, out gradLayerTheme, out chartLayerName, out chartLayerTheme);

                    if (chartLayerTheme is GIS_V4.Rendering.BarChartTheme)
                    {
                        var chart_theme = (GIS_V4.Rendering.BarChartTheme)chartLayerTheme;
                        string chart_style = "{\"BarCharts\" : [";
                        for (int j = 0; j < chart_theme.BarChartItems.Count; j++)
                        {
                            var c_column = chart_theme.BarChartItems[j].ColumnName;
                            var c_color = HexConverter(chart_theme.BarChartItems[j].Color);
                            chart_style += '{' + string.Format("\"title\":\"{0}\", \"color\":\"{1}\"", c_column, c_color) + '}' + ", ";
                        }
                        chart_style += "]}";
                        chart_style = chart_style.Replace(", ]", "]");
                        ViewBag.chart_style = new HtmlString(chart_style);                
                    }

                    if (gradLayerTheme is GIS_V4.Rendering.GradientTheme)
                    {
                        GIS_V4.Rendering.GradientTheme grad_theme = (GIS_V4.Rendering.GradientTheme)gradLayerTheme;
                        string min_value = grad_theme.Min.ToString();
                        string max_value = grad_theme.Max.ToString();

                        System.Drawing.SolidBrush min_brush = (System.Drawing.SolidBrush)((SharpMap.Styles.VectorStyle)grad_theme.MinStyle).Fill;
                        string min_r = min_brush.Color.R.ToString();
                        string min_g = min_brush.Color.G.ToString();
                        string min_b = min_brush.Color.B.ToString();

                        System.Drawing.SolidBrush max_brush = (System.Drawing.SolidBrush)((SharpMap.Styles.VectorStyle)grad_theme.MaxStyle).Fill;
                        string max_r = max_brush.Color.R.ToString();
                        string max_g = max_brush.Color.G.ToString();
                        string max_b = max_brush.Color.B.ToString();

                        ViewBag.grad_style = new HtmlString("{" + string.Format("\"type\":\"gradient\", \"min_value\":{0}, \"max_value\":{1}, \"min_color\":[{2}, {3}, {4}], \"max_color\":[{5}, {6}, {7}]", min_value, max_value, min_r, min_g, min_b, max_r, max_g, max_b) + "}");
                    }

                    if (gradLayerTheme is GIS_V4.Rendering.GraduatedTheme)
                    {
                        dataSet.Tables[0].Columns.Add("color");
                        dataSet.Tables[0].Columns.Add("symbol");


                        string grad_style = "{\"type\":\"graduated\", \"legend\" : [";
                        GIS_V4.Rendering.GraduatedTheme grad_theme = (GIS_V4.Rendering.GraduatedTheme)gradLayerTheme;
                        for (int i = 0; i < grad_theme.Rules.Count; i++)
                        {
                            string value_title = ((GIS_V4.Rendering.GraduatedTheme)gradLayerTheme).Rules[i].Title;
                            System.Drawing.SolidBrush value_brush = (System.Drawing.SolidBrush)((SharpMap.Styles.VectorStyle)grad_theme.Rules[i].Style).Fill;
                            var val_color = HexConverter(value_brush.Color);
                            var val_symbol = ((SharpMap.Styles.VectorStyle)grad_theme.Rules[i].Style).SymbolId;

                            grad_style += '{' + string.Format("\"title\":\"{0}\", \"color\":\"{1}\", \"symbol\":\"{2}\"", value_title, val_color, val_symbol) + '}' + ", ";
                        
                            var dataView = dataSet.DefaultViewManager.CreateDataView(dataSet.Tables[0]);
                            dataView.RowFilter = grad_theme.Rules[i].Condition;
                            for (int j = 0; j < dataView.Count; j++)
                            {
                                dataView[j].Row["color"] = val_color;
                                dataView[j].Row["symbol"] = val_symbol;
                            }                        
                        }
                        grad_style += "]}";
                        grad_style = grad_style.Replace(", ]", "]");
                        ViewBag.grad_style = new HtmlString(grad_style);
                    }


                    // Space in column fix
                    for (int j = 0; j < dataSet.Tables[1].Rows.Count; j++)
                    {
                        string c_new_name = dataSet.Tables[1].Rows[j]["ColumnDescription"].ToString();
                        c_new_name = c_new_name.TrimStart(); // Spaces in column name, comes outside
                        dataSet.Tables[0].Columns[c_new_name].ColumnName = c_new_name.TrimStart();
                        dataSet.Tables[1].Rows[j]["ColumnDescription"] = c_new_name.TrimStart();
                    }

                    using (var sqlConnection = new SqlConnection(connection))
                    {
                        try
                        {
                            sqlConnection.Open();
                            var CountryID = EidssSiteContext.Instance.CountryID;
                            SharpMap.Geometries.Geometry feature = Extents.GetGeomById(sqlConnection, "gisWKBCountry", CountryID);
                            if (feature != null) { feature = GeometryTransform.TransformGeometry(feature, GIS_V4.Common.CoordinateSystems.SphericalMercatorCS, GIS_V4.Common.CoordinateSystems.WGS84); }
                            SharpMap.Geometries.Point center_point = feature.GetBoundingBox().GetCentroid();
                            ViewBag.county_lon = center_point.X;
                            ViewBag.county_lat = center_point.Y;
                        }
                        catch (Exception)
                        {
                            ViewBag.county_lon = 0;
                            ViewBag.county_lat = 0;
                        }
                    }
                    //TEST

                    if (error.Length == 0)
                    {
                        var ds = new DataSet();
                        ds.Tables.Add(dataSet.Tables[1].Copy());
                        string json = GeoJSON.DataSetToJSON(ds);
                        string avr_json = GeoJSON.DataSetToJSON(ds);
                        ViewBag.avr_json = new HtmlString(avr_json);

                        string map_json = GetMapJson(dataSet.Tables[0]);
                        ViewBag.map_json = new HtmlString(map_json);
                        ViewBag.ColumnName = new HtmlString(dataSet.Tables[1].Rows[0].ItemArray[1].ToString());
                        ViewBag.ColumnDescription = new HtmlString(dataSet.Tables[1].Rows[0].ItemArray[1].ToString());
                        ViewBag.blnIsGradient = new HtmlString(dataSet.Tables[1].Rows[0].ItemArray[2].ToString());
                        ViewBag.gradLayerName = new HtmlString(gradLayerName);
                        ViewBag.chartLayerName = new HtmlString(chartLayerName);
                        ViewBag.blnIsChart = new HtmlString(dataSet.Tables[1].Rows[0].ItemArray[3].ToString());
                        ViewBag.map_localurl = Config.GetSetting("MapLocalUrl");
                        if (CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft) { ViewBag.left2right = 1; }
                        else { ViewBag.left2right = 0; }

                        return View("Index", dataSet);
                    }
                    return View("AvrServiceError", (object)error);
                }
                return View();
            }, Session.SessionID, layoutId, ViewLayoutController.StoragePrefix);
        }

        public ActionResult GetGeoDB(long layoutId)
        {
            var connectionCredentials = new bv.common.Configuration.ConnectionCredentials();
            string connection = connectionCredentials.ConnectionString;

            AvrServiceAccessability access = AvrServiceAccessability.Check();
            if (!access.IsOk)
            {
                return View("AvrServiceError", (object)access.ErrorMessage);
            }
            
                        return ObjectStorage.Using<AvrPivotViewModel, ActionResult>(viewModel =>
                        {
                            ViewBag.Title = string.Format(Translator.GetMessageString("webMapTitle"), viewModel.ViewHeader.LayoutName);

                            // have we anything selected in combo admin unit? 
                            if (!string.IsNullOrEmpty(viewModel.ViewHeader.MapAdminUnitViewColumn))
                            {
                                DataSet dataSet;
                                string error = ChartMapHelper.TryToPrepareMapData(viewModel, out dataSet);
                                if (error.Length > 0) return View("AvrServiceError", (object)error);

                                string path = Path.Combine(Server.MapPath("~/App_Data/ExportQueryFiles"), DateTime.Now.Ticks.ToString());
                                System.IO.Directory.CreateDirectory(path); path = path +'\\';
                                string SrvPath = Server.MapPath("");
                                eidss.gis.common.MapUtils.ExportToFGDB(dataSet, connection, path);

                                string filename = "Export.fgdb.zip";
                                string filepath = path + filename;
                                byte[] filedata = System.IO.File.ReadAllBytes(filepath);

                                var cd = new System.Net.Mime.ContentDisposition
                                {
                                    FileName = filename,
                                    Inline = true,
                                };

                                Response.AppendHeader("Content-Disposition", cd.ToString());

                                return File(filedata, "application/force-download");
                                }
                                return View();
                        }, Session.SessionID, layoutId, ViewLayoutController.StoragePrefix);
        }
        public ActionResult AvrMapInfo(double? lat, double? lon)
        {
            double llat = 0, llon = 0;
            if (lat.HasValue && lat != null) { llat = lat.Value; }
            if (lon.HasValue && lon != null) { llon = lon.Value; }

            return View(new eidss.avr.mweb.Models.AvrMapInfo(llon, llat));
        }


        private string GetMapJson(DataTable data)
        {
            var connectionCredentials = new bv.common.Configuration.ConnectionCredentials();
            string connection = connectionCredentials.ConnectionString;

            var column = new DataColumn { DataType = typeof(SqlGeometry), ColumnName = "geom" };
            data.Columns.Add(column);

            var sqlConnection = new SqlConnection(connection); sqlConnection.Open();
            string lTableName = CoordinatesUtils.GetWKBTableName(data, connection);

            const int srid = 4326;
            string ratio = "0"; // Settlement
            if (lTableName == "gisWKBRegion") { ratio = "50"; }
            if ((lTableName == "gisWKBRayon") || (lTableName == "gisWKBDistrict")) { ratio = "100"; }

            string strIds = string.Empty;
            if (data.Rows.Count > 0)
            {
                strIds = String.Join(",", data.AsEnumerable().Select(x => x.Field<long>("id").ToString()).ToArray());
                strIds = string.Format("idfsGeoObject in ({0}) and ", strIds);
                if ((data.PrimaryKey == null) || (data.PrimaryKey.Length == 0))
                {
                    var key = new DataColumn[1];
                    key[0] = data.Columns["id"];
                    data.PrimaryKey = key;
                }

                try // Try get it from precached table geomShape_4326
                {
                    string strBatchSql = "SELECT g.idfsGeoObject as id, g.geomShape_4326 as geom FROM " + lTableName + "Ready g WHERE " + strIds + " Ratio = " + ratio;
                    var cmdBatch = new SqlCommand(strBatchSql, sqlConnection);
                    using (SqlDataReader dr = cmdBatch.ExecuteReader())
                    {
                        var resBatch = new DataTable();

                        resBatch.Load(dr);
                        if ((resBatch != null) && resBatch.Rows.Count > 0)
                        {
                            if ((resBatch.PrimaryKey == null) || (resBatch.PrimaryKey.Length == 0))
                            {
                                var resBatchKey = new DataColumn[1];
                                resBatchKey[0] = resBatch.Columns["id"];
                                resBatch.PrimaryKey = resBatchKey;
                            }

                            data.Merge(resBatch, false, MissingSchemaAction.Ignore);
                        }
                    }
                }
                catch (Exception /*ex*/) { /*var strErr = ex.Message;*/ }

                foreach (var rEmptyGeom in data.Select("geom is null"))
                {
                    SharpMap.Geometries.Geometry feature = null;

                    long id = 0;
                    if (rEmptyGeom["id"].ToString() != "") { id = Int64.Parse(rEmptyGeom["id"].ToString()); }

                    // Try get it as usual
                    feature = Extents.GetGeomById(sqlConnection, lTableName, id);
                    if (feature != null)
                    {
                        feature = GeometryTransform.TransformGeometry(feature, GIS_V4.Common.CoordinateSystems.SphericalMercatorCS, GIS_V4.Common.CoordinateSystems.WGS84);
                        var wktGeometry = new System.Data.SqlTypes.SqlChars(feature.AsText());
                        rEmptyGeom["geom"] = SqlGeometry.STGeomFromText(wktGeometry, srid);
                    }

                    if (feature != null)
                    {
                        SharpMap.Geometries.Point point = feature.GetBoundingBox().GetCentroid();
                        rEmptyGeom["x"] = point.X;
                        rEmptyGeom["y"] = point.Y;
                    }
                    else
                    {
                        double x, y;
                        if (CoordinatesUtils.GetAdminUnitCoordinates(connection, id, out x, out y))
                        {
                            rEmptyGeom["x"] = x;
                            rEmptyGeom["y"] = y;
                        }
                    }
                }
            }

            sqlConnection.Close();

            var ds = new DataSet();
            ds.Tables.Add(data.Copy());

            string json = GeoJSON.DataSetToJSON(ds);
            json = json.Replace("\\r\\n", " ");

            return json;
        }

        private static String HexConverter(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

    }
}