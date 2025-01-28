using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using eidss.model.Resources;
using GIS_V4.Common;
using GIS_V4.Forms;
using GIS_V4.Tools;
using SharpMap.Geometries;
using bv.common;
using bv.common.Configuration;
using bv.common.Core;
using bv.common.db.Core;
using eidss.gis.Forms;

namespace eidss.gis.Tools
{
    public class GeoSearch : StateViewer 
    {
        public GeoSearch()
        {
            m_DockManager = new DockManager();
            m_DockManager.StartDocking += m_DockManager_StartDocking;
        }

        void m_SearchResultPanel_ClosingPanel(object sender, DockPanelCancelEventArgs e)
        {
            m_SearchResultPanel.Visible = false;
            e.Cancel = true;
        }

        void m_DockManager_StartDocking(object sender, DockPanelCancelEventArgs e)
        {
            e.Cancel = true;
        }

        #region Events

        //public delegate void NewSearchEventHandler(string searchString);

        //public event NewSearchEventHandler NewSearchCall;

        #endregion

        #region << ControlForVisualize >>

        private BarEditItem m_GeoSearchButtonEdit;
        private DockManager m_DockManager;
        private DockPanel m_SearchResultPanel;
        private SearchResult m_SearchResult;

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DefaultValue(null)]
        public BarEditItem ControlForVisualize
        {
            get { return m_GeoSearchButtonEdit; }
            set
            {
                m_GeoSearchButtonEdit = value;
                if (m_GeoSearchButtonEdit != null)
                {
                    
                }
            }
        }

        private MapImage m_MapImage;
        public new MapImage MapControl
        {
            get { return m_MapImage; }
            set
            {
                m_MapImage = value;
                if (m_MapImage != null)
                    if (m_MapImage.Parent != null)
                    {
                        var form = m_MapImage.Parent as MapControl;
                        if (form != null)
                            m_DockManager.Form = (ContainerControl) m_MapImage.Parent;
                    }
            }
        }

        public class SearchItem
        {
            public SearchItem(string region, string rayon, string settlement, long id, Geometry shape)
            {
                Region = region;
                Rayon = rayon;
                Settlement = settlement;
                Id = id;
                Shape = shape;
            }

            public string Region { get; set; }
            public string Rayon { get; set; }
            public string Settlement { get; set; }
            public long Id { get; set; }
            public Geometry Shape { get; set; }

            public string Result
            {
                get
                {
                    var strReg = EidssMessages.GetForCurrentLang("gis_GeoSearch_Region", "region");
                    var strRay = EidssMessages.GetForCurrentLang("gis_GeoSearch_Rayon", "rayon");
                    var strStt = EidssMessages.GetForCurrentLang("gis_GeoSearch_Settlement", "settlement");

                    if (Settlement.Trim() != "")
                    {
                        return string.Format("{0} {5}, {1} {4}, {2} {3}", Settlement, Rayon, Region, strReg, strRay,strStt);
                    }
                   
                    if (Rayon.Trim() != "" & Settlement.Trim() == "")
                        return string.Format("{0} {3}, {1} {2}", Rayon, Region, strReg, strRay);

                    return string.Format("{0} {1}", Region, strReg);
                }
            }
            
        }

        //public List<string> SearchResultItems;
        
        public List<SearchItem> SearchResultList;

        //private int m_SearchPage = 0;
        private int m_SearchRegNum = 0;
        private int m_SearchRnNum = 0;
        private int m_SearchStNum = 0;
        private const int GeoObjPerPage = 10;
        
        private string m_SearchString = string.Empty;

        public void NewSearch(string value)
        {
            m_SearchRegNum = 0;
            m_SearchRnNum = 0;
            m_SearchStNum = 0;
            m_SearchString = value;
            if (!Search())
                MessageBox.Show("Nothing is found", "Search");
        }

        public bool Search()
        {
            var value = m_SearchString;
            if (value == string.Empty) return false;

            SearchResultList = new List<SearchItem>();
            
            try
            {
                var strConnect = ConnectionManager.DefaultInstance.ConnectionString;
                using (SqlConnection sqlConnection = new SqlConnection(strConnect))
                {
                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        sqlConnection.Open();
                    }

                    int objNum = 0;

                    #region Search in Regions

                    using (IDbCommand sqlCommand1 = new SqlCommand("spgisSearchInRegion", sqlConnection))
                    {
                        sqlCommand1.CommandType = CommandType.StoredProcedure;

                        var pName1 = sqlCommand1.CreateParameter();
                        pName1.ParameterName = "@GisObjectName";
                        pName1.Direction = ParameterDirection.Input;
                        pName1.DbType = DbType.String;
                        pName1.Value = value;

                        var pStartIndex1 = sqlCommand1.CreateParameter();
                        pStartIndex1.ParameterName = "@StartIndex";
                        pStartIndex1.Direction = ParameterDirection.Input;
                        pStartIndex1.DbType = DbType.Int64;
                        pStartIndex1.Value = m_SearchRegNum + 1;

                        var pEndIndex1 = sqlCommand1.CreateParameter();
                        pEndIndex1.ParameterName = "@EndIndex";
                        pEndIndex1.Direction = ParameterDirection.Input;
                        pEndIndex1.DbType = DbType.Int64;
                        pEndIndex1.Value = m_SearchRegNum + GeoObjPerPage;

                        sqlCommand1.Parameters.Clear();
                        sqlCommand1.Parameters.Add(pName1);
                        sqlCommand1.Parameters.Add(pStartIndex1);
                        sqlCommand1.Parameters.Add(pEndIndex1);

                        using (IDataReader sqlDataReader1 = sqlCommand1.ExecuteReader())
                        {
                            while (sqlDataReader1.Read())
                            {
                                var geom =
                                    SharpMap.Converters.WellKnownBinary.GeometryFromWKB.Parse((byte[])sqlDataReader1[2]);
                                SearchResultList.Add(new SearchItem(sqlDataReader1[1].ToString(), string.Empty, string.Empty,
                                                                    long.Parse(sqlDataReader1[0].ToString()), geom));
                                objNum++;
                                m_SearchRegNum++;
                            }
                        }
                    }

                    #endregion

                    if (objNum < GeoObjPerPage)
                    {
                        #region Search in Rayons

                        using (IDbCommand sqlCommand2 = new SqlCommand("spgisSearchInRayon", sqlConnection))
                        {
                            sqlCommand2.CommandType = CommandType.StoredProcedure;

                            var pName2 = sqlCommand2.CreateParameter();
                            pName2.ParameterName = "@GisObjectName";
                            pName2.Direction = ParameterDirection.Input;
                            pName2.DbType = DbType.String;
                            pName2.Value = value;

                            var pStartIndex2 = sqlCommand2.CreateParameter();
                            pStartIndex2.ParameterName = "@StartIndex";
                            pStartIndex2.Direction = ParameterDirection.Input;
                            pStartIndex2.DbType = DbType.Int64;
                            pStartIndex2.Value = m_SearchRnNum + 1;

                            var pEndIndex2 = sqlCommand2.CreateParameter();
                            pEndIndex2.ParameterName = "@EndIndex";
                            pEndIndex2.Direction = ParameterDirection.Input;
                            pEndIndex2.DbType = DbType.Int64;
                            pEndIndex2.Value = m_SearchRnNum + GeoObjPerPage - objNum;

                            sqlCommand2.Parameters.Clear();
                            sqlCommand2.Parameters.Add(pName2);
                            sqlCommand2.Parameters.Add(pStartIndex2);
                            sqlCommand2.Parameters.Add(pEndIndex2);

                            objNum = 0;

                            using (IDataReader sqlDataReader2 = sqlCommand2.ExecuteReader())
                            {
                                while (sqlDataReader2.Read())
                                {
                                    var geom = SharpMap.Converters.WellKnownBinary.GeometryFromWKB.Parse((byte[])sqlDataReader2[5]);
                                    SearchResultList.Add(new SearchItem(sqlDataReader2[4].ToString(), sqlDataReader2[2].ToString(),
                                                                        string.Empty, long.Parse(sqlDataReader2[0].ToString()), geom));
                                    objNum++;
                                    m_SearchRnNum++;
                                }
                            }
                        }

                        #endregion
                    }

                    if (objNum < GeoObjPerPage)
                    {
                        #region Search in settlements

                        using (IDbCommand sqlCommand3 = new SqlCommand("spgisSearchInSettlement", sqlConnection))
                        {
                            sqlCommand3.CommandType = CommandType.StoredProcedure;

                            var pName3 = sqlCommand3.CreateParameter();
                            pName3.ParameterName = "@GisObjectName";
                            pName3.Direction = ParameterDirection.Input;
                            pName3.DbType = DbType.String;
                            pName3.Value = value;

                            var pStartIndex3 = sqlCommand3.CreateParameter();
                            pStartIndex3.ParameterName = "@StartIndex";
                            pStartIndex3.Direction = ParameterDirection.Input;
                            pStartIndex3.DbType = DbType.Int64;
                            pStartIndex3.Value = m_SearchStNum + 1;

                            var pEndIndex3 = sqlCommand3.CreateParameter();
                            pEndIndex3.ParameterName = "@EndIndex";
                            pEndIndex3.Direction = ParameterDirection.Input;
                            pEndIndex3.DbType = DbType.Int64;
                            pEndIndex3.Value = m_SearchStNum + GeoObjPerPage - objNum;

                            sqlCommand3.Parameters.Clear();
                            sqlCommand3.Parameters.Add(pName3);
                            sqlCommand3.Parameters.Add(pStartIndex3);
                            sqlCommand3.Parameters.Add(pEndIndex3);

                            objNum = 0;

                            using (IDataReader sqlDataReader3 = sqlCommand3.ExecuteReader())
                            {
                                while (sqlDataReader3.Read())
                                {
                                    var geom = SharpMap.Converters.WellKnownBinary.GeometryFromWKB.Parse((byte[])sqlDataReader3[5]);
                                    SearchResultList.Add(new SearchItem(sqlDataReader3[4].ToString(), sqlDataReader3[3].ToString(),
                                                                        sqlDataReader3[2].ToString(),
                                                                        long.Parse(sqlDataReader3[0].ToString()), geom));
                                    objNum++;
                                    m_SearchStNum++;
                                }
                            }
                        }

                        #endregion
                    }

                    if (sqlConnection.State == ConnectionState.Open)
                    {
                        sqlConnection.Close();
                    }
                }

                if (SearchResultList.Count == 0) return false;

                if (m_SearchResultPanel == null)
                {
                    if (!RtlHelper.IsRtl)
                        m_SearchResultPanel = m_DockManager.AddPanel(DockingStyle.Left);
                    else
                        m_SearchResultPanel = m_DockManager.AddPanel(DockingStyle.Right);
                    //m_SearchResultPanel.Visible = false;
                    m_SearchResultPanel.Text = EidssMessages.GetForCurrentLang("gis_Tools_Geosearch_Btn", "Search"); 
                    m_DockManager.DockingOptions.ShowAutoHideButton = false;
                    m_SearchResult = new SearchResult(this);
                    m_SearchResultPanel.Controls.Add(m_SearchResult);
                    m_SearchResult.Dock = DockStyle.Fill;

                    m_SearchResultPanel.ClosingPanel += m_SearchResultPanel_ClosingPanel;
                }

                    m_SearchResult.RefreshList();
                
            }
            catch (Exception ex)
            {
                throw new Exception("Search error: " + ex.Message);
            }
            
            return true;
        }
        
        #endregion
    }
}
