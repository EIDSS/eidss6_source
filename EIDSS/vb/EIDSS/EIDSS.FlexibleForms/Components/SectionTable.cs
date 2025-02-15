using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using bv.common;
using bv.common.Configuration;
using bv.common.Core;
using DevExpress.Data;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using EIDSS.FlexibleForms.DataBase;
using EIDSS.FlexibleForms.Helpers;
using bv.common.Resources;
using bv.winclient.Localization;

namespace EIDSS.FlexibleForms.Components
{
    /// <summary>
    /// Табличная секция
    /// </summary>
    public class SectionTable : Section
    {
        private GridBand m_SelectedBand;
        private GridControl m_GridControlMain;
        internal AdvBandedGridView GridViewMain { get; set; }
        private RepositoryItemTextEdit m_RepositoryItemTextEdit1;

        /// <summary>
        /// Визуальная таблица
        /// </summary>
        public GridControl GridControlMain
        {
            get { return RootSection.m_GridControlMain; }
        }

        /// <summary>
        /// Таблица-хранилище для пользовательских и демонстрационных данных
        /// </summary>
        internal DataTable TblUserData { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public SectionTable()
        {
            NowLoading = true;
            InitializeComponent();
            GridViewMain.Tag = this; //это нужно для ввода данных
            DeltaStub = 50;
            Toolbar.Visible = false;
            GridViewMain.CellValueChanged += OnGridViewMainCellValueChanged;
            NowLoading = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnGridViewMainCellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (CellValueChanged != null) CellValueChanged(sender, e);
        }

        //TODO возможно, есть более элегантный способ
        internal void FinishInit()
        {
            GridViewMain.DragObjectOver += OnGridViewMainDragObjectOver;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnGridViewMainDragObjectOver(object sender, DragObjectOverEventArgs e)
        {
            e.DropInfo.Valid = e.DropInfo.Index >= 0;
        }

        /// <summary>
        /// Ссылка на корневую секцию (т.е. на сам контрол, лежащий на рабочем столе шаблона)
        /// </summary>
        internal SectionTable RootSection { get; set; }

        /// <summary>
        /// Источник данных для данной табличной секции
        /// </summary>
        public DataTable DataTable
        {
            get { return RootSection.m_GridControlMain.DataSource as DataTable; }
            set { RootSection.m_GridControlMain.DataSource = value; }
        }

        /// <summary>
        /// Возвращает бэнд, который сейчас выбран
        /// </summary>
        internal GridBand SelectedBand
        {
            get { return m_SelectedBand; }
            set
            {
                if (m_SelectedBand == value) return;
                if (m_SelectedBand != null)
                {
                    m_SelectedBand.View.GridControl.Invalidate();
                    m_SelectedBand = null;
                }
                m_SelectedBand = value;
            }
        }

        /// <summary>
        /// Добавляет секцию-band
        /// </summary>
        /// <param name="rowSectionTemplate"></param>
        public void AddBand(FlexibleFormsDS.SectionTemplateRow rowSectionTemplate)
        {
            //определяем, не добавлен ли уже этот band
            if (FindBand(rowSectionTemplate.idfsSection, true) != null) return;

            #region Создаём новый band

            var band = new GridBand
                           {
                               Caption = rowSectionTemplate.NationalName,
                               Name = String.Format("band{0}", rowSectionTemplate.idfsSection),
                               Width = !rowSectionTemplate.IsintWidthNull() ? rowSectionTemplate.intWidth : 80,
                               Tag = rowSectionTemplate,
                               Visible = true
                           };

            #endregion

            //ищем родительский бэнд
            var parentBand = FindBand(rowSectionTemplate.idfsParentSection, true);
            if (parentBand != null)
            {
                parentBand.Children.Add(band);
                SetBandsVisibleIndex(parentBand.Children);
                ReCalculateColumnOrder(parentBand.Children);
            }
            else
            {
                var stToppest = GetParentSectionTable();
                if (stToppest != null)
                {
                    stToppest.GridViewMain.Bands.Add(band);
                    SetBandsVisibleIndex(stToppest.GridViewMain.Bands);
                    ReCalculateColumnOrder(stToppest.GridViewMain.Bands);
                }
            }

            //если эта секция "заморожена", то восстановим это состояние
            if (rowSectionTemplate.blnFreeze)
            {
                band.RootBand.Fixed = FixedStyle.Left;
            }
        }

        /// <summary>
        /// Величина смещения по индексам из-за боковика
        /// </summary>
        public static int DeltaStub { get; set; }

        /// <summary>
        /// Внутреняя инициализация табличной секции
        /// </summary>
        internal void Init()
        {
            //в таблице должен быть только один датасет. Нижние секции ссылаются на датасет самой верхней секции
            //если эта табличная секция является верхней, то создадим ей источник данных для хранения строки для демо-данных
            //и родительская секция обязательно должна быть табличной!!
            if ((ParentSection != null) && (ParentSection is SectionTable))
            {
                var parentSection = GetParentSectionTable();
                TblUserData = parentSection.TblUserData;
            }
            else
            {
                TblUserData = new DataTable();
                //TblUserData.Rows.Add(TblUserData.NewRow());

                //если эта секция корневая и без фиксированного боковика, то дадим возможность добавлять и удалять строки
                if (!IsFixedStubSection)
                {
                    Toolbar.Visible = true;
                }
            }

            m_GridControlMain.DataSource = TblUserData;
        }

        /// <summary>
        /// Находит и возвращает самый верхний SectionTable
        /// Если не находит такового, то возвращает null.
        /// </summary>
        /// <returns></returns>
        private SectionTable GetParentSectionTable()
        {
            var result = this;
            if (ParentSection != null)
            {
                if (ParentSection is SectionTable)
                    result = ((SectionTable) ParentSection).GetParentSectionTable();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        private void DeleteBand(GridBand band, bool deleteLinkedData)
        {
            //удаляем зависимые банды
            if (band.Children.Count > 0)
            {
                for (var i = band.Children.Count - 1; i >= 0; i--)
                {
                    DeleteBand(band.Children[i], deleteLinkedData);
                }
            }
           
            //удаляем связанные данные
            //определим, на каком фиктивном столбце стоит курсор
            if (band.Tag is FlexibleFormsDS.ParameterTemplateRow)
            {
                //у бэнда, который столбец, есть один дочерний столбец
                //удаляем столбец из таблицы демо-данных
                if (DataTable != null) DataTable.Columns.Remove(band.Columns[0].Name);
                //удаляем запись из таблицы
                if (deleteLinkedData)
                {
                    //TODO ссылка на этот параметр должна быть удалена из ParameterList родительского ffRender. Сейчас это делается.
                    var parameterTemplateRow = (FlexibleFormsDS.ParameterTemplateRow) band.Tag;
                    //удаляем записи для всех языков
                    MainDbService.DeleteParameterTemplate(parameterTemplateRow.idfsFormTemplate, parameterTemplateRow.idfsParameter, String.Empty);
                }
            }
            if (band.Tag is FlexibleFormsDS.SectionTemplateRow)
            {
                //удаляем запись из таблицы
                if (deleteLinkedData)
                {
                    var sectionTemplateRow = (FlexibleFormsDS.SectionTemplateRow)band.Tag;
                    //удаляем записи для всех языков
                    MainDbService.DeleteSectionTemplate(sectionTemplateRow.idfsFormTemplate, sectionTemplateRow.idfsSection, String.Empty);
                }
            }
            //удаляем столбцы
            band.Columns.Clear();

            if (band.ParentBand != null)
                band.ParentBand.Children.Remove(band);
            else
                RootSection.GridViewMain.Bands.Remove(band);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deleteLinkedData"></param>
        public override void Delete(bool deleteLinkedData)
        {
            if (Utils.IsEmpty(SelectedBand) && IdfsSection.HasValue)
            {
                //если у табличной секции нет дочерних секций и дочерних параметров, то её саму можно удалить
                //и она реально расположена на рабочем столе шаблона (Parent != null)
                if ((RootSection.GridViewMain.Bands.Count == 0))
                {
                    var gridBandSection = HelperFunctions.GetGridBandByName(String.Format("band{0}", IdfsSection.Value), RootSection.GridViewMain.Bands);
                    //если бэнд не нашёлся, то эта табличная секция наивысшего уровня, которая является контейнером
                    if ((gridBandSection == null) && (Parent != null))
                    {
                        Parent.Controls.Remove(this);
                    }
                    else if (SectionTemplateRow.IsRowAlive())
                    {
                        //ищем родительский бэнд
                        var parentBand = RootSection.FindBand(SectionTemplateRow.idfsParentSection, true);
                        if (parentBand != null)
                        {
                            parentBand.Children.Remove(gridBandSection);
                            ReCalculateColumnOrder(parentBand.Children);
                        }
                        else
                        {
                            var stToppest = GetParentSectionTable();
                            if (stToppest != null)
                            {
                                //если не удалось определить бэнд этой секции, поскольку не найден её родитель, то пробуем отыскать на самом верхнем уровне
                                if (gridBandSection == null) gridBandSection = HelperFunctions.GetGridBandByName(String.Format("band{0}", IdfsSection.Value), stToppest.GridViewMain.Bands);
                                if (gridBandSection != null) stToppest.GridViewMain.Bands.Remove(gridBandSection);
                                ReCalculateColumnOrder(stToppest.GridViewMain.Bands);
                            }
                        }
                    }

                    //удаляем данные, связанные с этой табличной секцией
                    if (deleteLinkedData)
                    {
                        var sectionTemplateRow = (FlexibleFormsDS.SectionTemplateRow)Tag;
                        if (sectionTemplateRow.IsRowAlive()) MainDbService.DeleteSectionTemplate(sectionTemplateRow.idfsFormTemplate, sectionTemplateRow.idfsSection, String.Empty);
                    }
                }
            }
            else
            {
                DeleteBand(SelectedBand, deleteLinkedData);
                SelectedBand = null;
            }
        }

        /// <summary>
        /// Удаляет данную секцию, все зависимые объекты, а также их содержимое
        /// </summary>
        public override void Delete()
        {
            Delete(true);
        }

        /// <summary>
        /// Определяет нужный порядок бэнда
        /// </summary>
        /// <param name="gridBand"></param>
        /// <returns></returns>
        private int? GetBandOrder(GridBand gridBand)
        {
            var rowParameterTemplate = gridBand.Tag as FlexibleFormsDS.ParameterTemplateRow;
            var sectionTemplateRow = gridBand.Tag as FlexibleFormsDS.SectionTemplateRow;
            if ((rowParameterTemplate == null) && (sectionTemplateRow == null)) return null;
            return sectionTemplateRow != null
                            ? sectionTemplateRow.intOrder
                            : rowParameterTemplate.intOrder;
        }

        /// <summary>
        /// Обновляет положение бэндов таблицы в соответствии с их порядком (intOrder)
        /// </summary>
        /// <param name="gridBandCollection"></param>
        internal void RefreshBandPositions(GridBandCollection gridBandCollection)
        {
            var sortedBands = new SortedDictionary<int, GridBand>();

            foreach (GridBand gridBand in gridBandCollection)
            {
                var order = GetBandOrder(gridBand);
                if (!order.HasValue) continue;
                //если такой порядок уже есть в коллекции, то это ошибка
                //не включаем этот столбец в сортировку
                if (sortedBands.ContainsKey(order.Value)) continue;
                sortedBands.Add(order.Value, gridBand);
            }

            foreach (var keyValuePair in sortedBands)
            {
                gridBandCollection.MoveTo(keyValuePair.Key + DeltaStub, keyValuePair.Value);
            }
        }

        /// <summary>
        /// Добавляет параметр
        /// </summary>
        /// <param name="parameter"></param>
        public void AddParameter(Parameter parameter)
        {
            var rowParameterTemplate = parameter.Tag as FlexibleFormsDS.ParameterTemplateRow;
            if (rowParameterTemplate == null) return;
            //теперь проставляем ему реальные данные выбранного Параметра
            //определяем, не добавлен ли уже этот параметр
            if (FindParameterColumn(rowParameterTemplate) != null) return;

            //добавляем band, который обозначает Параметр, и к нему единственный столбец
            //сей столбец является фиктивным и требуется только для того, чтобы можно было разместить данные

            #region Создаём новый band

            var band = new GridBand
                                {
                                    Caption = rowParameterTemplate.NationalName,
                                    Name = String.Format("band{0}", rowParameterTemplate.idfsParameter),
                                    Tag = rowParameterTemplate,
                                    Width = !rowParameterTemplate.IsintWidthNull() ? rowParameterTemplate.intWidth : 80,
                                    Visible = true
                                };

            #endregion

            #region Создаём фиктивный столбец

            var column = new BandedGridColumn
                             {
                                 Name = String.Format("column{0}", rowParameterTemplate.idfsParameter),
                                 Caption = rowParameterTemplate.NationalName,
                                 Tag = band,
                                 Visible = true
                             };
            column.OptionsColumn.AllowMove = false;
            column.OptionsColumn.ShowCaption = false;
            column.Width = rowParameterTemplate.intWidth;

            #region Определяем управляющий контрол

            var editor = ParameterControlTypeHelper.ConvertToParameterControlType(rowParameterTemplate.idfsEditor);
            RepositoryItem control = null;
            //тип для столбца демо-данных, связанных с этим столбцом
            var typeForDataColumn = typeof(String);
            
            switch (editor)
            {
                case FFParameterEditors.TextBox:
                    {
                        control = new RepositoryItemTextEdit();
                        var repositoryItemTextEdit = (RepositoryItemTextEdit)control;
                        var result = HelperFunctions.GetMaskPropertiesForParameter(rowParameterTemplate.idfsParameterType);
                        repositoryItemTextEdit.Mask.EditMask = result.EditMask;
                        repositoryItemTextEdit.Mask.MaskType = result.MaskType;
                        repositoryItemTextEdit.Mask.ShowPlaceHolders = false;
                    }
                    break;
                case FFParameterEditors.ComboBox:
                    {
                        control = new RepositoryItemLookUpEdit {NullText = String.Empty};
                        //связываем с выборкой данных
                        var le = (RepositoryItemLookUpEdit)control;
                        le.ValueMember = "idfsValue";
                        le.DisplayMember = "strValueCaption";
                        le.DataSource = new DataView(parameter.ParameterSelectListFull);
                        le.ShowHeader = le.ShowFooter = false;
                        var col = new LookUpColumnInfo("strValueCaption", 120) {SortOrder = ColumnSortOrder.Ascending};
                        le.Columns.Add(col);
                        var eb = new EditorButton(ButtonPredefines.Delete);
                        le.Buttons.Add(eb);
                        le.ButtonClick += OnButtonControlClick;
                        le.QueryPopUp += OnLeQueryPopUp;
                        le.QueryCloseUp += OnLeQueryCloseUp;
                        le.Tag = parameter;//rowParameterTemplate;
                    }
                    break;
                case FFParameterEditors.CheckBox:
                    control = new RepositoryItemCheckEdit();
                    ((RepositoryItemCheckEdit) control).NullStyle = StyleIndeterminate.Unchecked;
                    break;
                case FFParameterEditors.Date:
                    {
                        control = new RepositoryItemDateEdit();
                        var de = (RepositoryItemDateEdit)control;
                        typeForDataColumn = typeof (DateTime);
                        var eb = new EditorButton(ButtonPredefines.Delete);
                        de.Buttons.Add(eb);
                        DxControlsHelper.InitRepositoryDateEdit(de, BaseSettings.ShowDateTimeFormatAsNullText);
                        de.ButtonClick += OnButtonControlClick;
                    }
                    break;
                case FFParameterEditors.DateTime:
                    {
                        control = new RepositoryItemTimeEdit();
                        var de = (RepositoryItemTimeEdit)control;
                        de.EditMask = "g";
                        typeForDataColumn = typeof (DateTime);
                        var eb = new EditorButton(ButtonPredefines.Delete);
                        de.Buttons.Add(eb);
                        de.NullText = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                        de.ButtonClick += OnButtonControlClick;
                    }
                    break;
                case FFParameterEditors.Memo:
                    control = new RepositoryItemMemoEdit();
                    break;
                case FFParameterEditors.UpDown:
                    {
                        control = new RepositoryItemSpinEdit();
                        typeForDataColumn = typeof (int);
                        var se = (RepositoryItemSpinEdit)control;
                        se.MaxValue = 99000000;
                        se.MinValue = 0;
                        var eb = new EditorButton(ButtonPredefines.Delete);
                        se.Buttons.Add(eb);
                        se.ButtonClick += OnButtonControlClick;
                        control = new RepositoryItemTextEdit();
                        var result = HelperFunctions.GetMaskPropertiesForParameter(rowParameterTemplate.idfsParameterType);
                        se.Mask.EditMask = result.EditMask;
                        se.Mask.MaskType = result.MaskType;
                        se.Mask.ShowPlaceHolders = false;
                    }
                    break;
            }

            if (control != null)
            {
                m_GridControlMain.RepositoryItems.Add(control);
                column.ColumnEdit = control;
                //создадим событие, которое будет срабатывать когда пользователь что-либо менял в контроле
                control.EditValueChanging += OnControlEditValueChanging;
            }

            #endregion

            #endregion

            //ищем родительский бэнд (секцию!)
            var parentBand = FindBand(rowParameterTemplate.idfsSection, true);
            if (parentBand != null)
            {
                parentBand.Children.Add(band);
                SetBandsVisibleIndex(parentBand.Children);
                ReCalculateColumnOrder(parentBand.Children);
            }
            else
            {
                GridViewMain.Bands.Add(band);
                SetBandsVisibleIndex(GridViewMain.Bands);
                ReCalculateColumnOrder(GridViewMain.Bands);
            }
            
            //добавляем в band этот столбец
            //нужно делать добавление именно в такой последовательности, иначе не будет визуализирован столбец
            band.Columns.Add(column);
            
            if (RootSection != null)
            {
                //добавляем в таблицу демо-данных ещё столбец, связанный с этим столбцом
                if (DataTable != null)
                {
                    var dataColumn = new DataColumn(column.Name, typeForDataColumn);
                    DataTable.Columns.Add(dataColumn);
                    column.FieldName = dataColumn.ColumnName;
                    //присвоим демо-данные
                    //DataTable.Rows[0][dataColumn.ColumnName] = initValue;
                }
                //определим редактируемость столбца
                column.OptionsColumn.AllowFocus = band.RootBand.Fixed != FixedStyle.Left;
            }

            //если этот параметр "заморожен", то восстановим это состояние
            if (rowParameterTemplate.blnFreeze)
            {
                band.RootBand.Fixed = FixedStyle.Left;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnLeQueryCloseUp(object sender, CancelEventArgs e)
        {
            var le = sender as LookUpEdit;
            if (le != null)
            {
                var dv = (DataView)le.Properties.DataSource;
                dv.RowFilter = String.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnLeQueryPopUp(object sender, CancelEventArgs e)
        {
            var le = sender as LookUpEdit;

            if (le == null)
            {
                e.Cancel = true;
                return;
            }
            var gc = le.Parent as GridControl;
            if (gc == null) return;
            var dv = (DataView)le.Properties.DataSource;
            var obj = ((GridView) gc.MainView).GetFocusedValue();
            if (obj != null)
            {
                long val;
                
                if (Int64.TryParse(obj.ToString(), out val))
                {
                    le.EditValue = val;
                    dv.RowFilter = String.Format("intRowStatus = 0 or idfsValue = {0}", val);
                }
                else
                {
                    dv.RowFilter = "intRowStatus = 0";
                }
            }
            else
            {
                dv.RowFilter = "intRowStatus = 0";
            }
        }

        /// <summary>
        /// Событие на изменение значения в ячейке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnControlEditValueChanging(object sender, ChangingEventArgs e)
        {
            if (EditValueChanging != null) EditValueChanging(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idfsSectionTemplate"> </param>
        /// <param name="numRow"></param>
        void OnRemoveRow(long idfsSectionTemplate, int numRow)
        {
            if (RemoveRow != null) RemoveRow(idfsSectionTemplate, numRow);
        }

        public delegate void EditValueChangingDelegate(object sender, ChangingEventArgs e);

        /// <summary>
        /// Событие срабатывает, когда меняется значение в ячейке
        /// </summary>
        public event EditValueChangingDelegate EditValueChanging;


        public delegate void RemoveRowDelegate(long idfsSectionTemplate, int numRow);

        /// <summary>
        /// Нужно вызвать событие, чтобы удалить значение в ячейке автоматическим способом.
        /// Ручное удаление происходит за счёт события изменения значения в ячейке.
        /// </summary>
        public event RemoveRowDelegate RemoveRow;

        /// <summary>
        /// Нажатие на кнопку на управляющем контроле, если он Lookup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnButtonControlClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                GridViewMain.SetFocusedValue(DBNull.Value);
            }
        }



        /// <summary>
        /// Устанавливает значение ячейки в таблице
        /// </summary>
        /// <param name="intRow"></param>
        /// <param name="parameter"></param>
        /// <param name="value"></param>
        internal void SetTableValue(Parameter parameter, int intRow, object value)
        {
            var column = parameter.RootSection.FindParameterColumn(parameter.ParameterTemplateRow);
            //если строк недостаточно в таблице, чтобы отобразить переданное значение, то добавим строк
            if ((intRow + 1) > TblUserData.Rows.Count)
            {
                for (var i = TblUserData.Rows.Count; i < (intRow + 1); i++)
                {
                    TblUserData.Rows.Add(new object[] {});
                }
            }
            TblUserData.Rows[intRow][column.Name] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selected"></param>
        internal override void ShowSelected(bool selected)
        {
            base.ShowSelected(selected);
            if (!selected) SelectedBand = null;
        }

        /// <summary>
        /// Добавление параметра или секции (могут добавляться только табличные секции)
        /// </summary>
        /// <param name="child"></param>
        public override void Add(Control child)
        {
            // Если это секция
            if (child is SectionTable)
            {
                ((SectionTable) child).RootSection = RootSection;
                AddBand((FlexibleFormsDS.SectionTemplateRow) child.Tag);
            }
            //Если это параметр
            else if (child is Parameter)
            {
                AddParameter((Parameter)child);
            }

        }

        /// <summary>
        /// Вспомогательный метод для поиска объекта среди дочерних компонент контрола и в нём самом
        /// </summary>
        /// <param name="id">id параметра или секции</param>
        /// <param name="isSection">Ищем секцию?</param>
        /// <returns></returns>
        internal GridBand FindBand(long id, bool isSection)
        {
            GridBand result = null;

            //выявляем самое верхнее хранилище бэндов
            var stToppest = GetParentSectionTable();

            if (isSection)
            {
                foreach (GridBand band in stToppest.GridViewMain.Bands)
                {
                    result = FindSectionInBand(band, id);
                    if (result != null) break;
                }
            }
            else
            {
                foreach (GridBand band in stToppest.GridViewMain.Bands)
                {
                    result = FindParameterInBand(band, id);
                    if (result != null) break;
                }
            }


            return result;
        }

        /// <summary>
        /// Отыскивает параметр в самом бэнде и его потомках-бэндах
        /// </summary>
        /// <param name="band"></param>
        /// <param name="idfsparameter"></param>
        /// <returns></returns>
        private GridBand FindParameterInBand(GridBand band, long idfsparameter)
        {
            GridBand result = null;

            if ((band.Tag != null) && (band.Tag is FlexibleFormsDS.ParameterTemplateRow) &&
                ((((FlexibleFormsDS.ParameterTemplateRow)band.Tag).idfsParameter.Equals(idfsparameter))))
            {
                result = band;
            }
            else
            {
                foreach (GridBand b in band.Children)
                {
                    result = FindParameterInBand(b, idfsparameter);
                    if (result != null) break;
                }
            }

            return result;
        }

        /// <summary>
        /// Отыскивает секцию в самом бэнде и его потомках-бэндах
        /// </summary>
        /// <param name="band"></param>
        /// <param name="idfssection"></param>
        /// <returns></returns>
        private GridBand FindSectionInBand(GridBand band, long idfssection)
        {
            GridBand result = null;

            if ((band.Tag != null) && (band.Tag is FlexibleFormsDS.SectionTemplateRow) &&
                ((((FlexibleFormsDS.SectionTemplateRow) band.Tag).idfsSection.Equals(idfssection))))
            {
                result = band;
            }
            else
            {
                foreach (GridBand b in band.Children)
                {
                    result = FindSectionInBand(b, idfssection);
                    if (result != null) break;
                }
            }

            return result;
        }

        /// <summary>
        /// Является ли эта секция секцией с предустановленным боковиком
        /// </summary>
        public bool IsFixedStubSection { get; internal set; }

        /// <summary>
        /// Поиск объекта среди дочерних компонент контрола и в нём самом
        /// </summary>
        /// <param name="rowParameter"></param>
        /// <returns></returns>
        internal BandedGridColumn FindParameterColumn(FlexibleFormsDS.ParameterTemplateRow rowParameter)
        {
            BandedGridColumn result = null;

            //выявляем самое верхнее хранилище бэндов
            var stToppest = GetParentSectionTable();

            foreach (GridBand band in stToppest.GridViewMain.Bands)
            {
                var bandParameter = FindParameterInBand(band, rowParameter);
                if (bandParameter != null)
                {
                    //там всегда есть один столбец 
                    result = bandParameter.Columns[0];
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Отыскивает секцию в самом бэнде и его потомках-бэндах
        /// </summary>
        /// <param name="band"></param>
        /// <param name="rowParameter"></param>
        /// <returns></returns>
        private GridBand FindParameterInBand(GridBand band, FlexibleFormsDS.ParameterTemplateRow rowParameter)
        {
            GridBand result = null;

            if ((band.Tag != null) && (band.Tag.Equals(rowParameter)))
            {
                result = band;
            }
            else
            {
                foreach (GridBand b in band.Children)
                {
                    result = FindParameterInBand(b, rowParameter);
                    if (result != null) break;
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Control TopControl()
        {
            return m_GridControlMain;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal override bool IsManagedControl(Control ctl)
        {
            return m_GridControlMain == ctl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="coord"></param>
        private void CalculateActive(Control parent, Point coord)
        {
            var hit = ((GridControl)parent).DefaultView.CalcHitInfo(coord);
            if (!(hit is BandedGridHitInfo)) return;
            var bhit = (BandedGridHitInfo)hit;
            SelectedBand = (bhit.Band != null) && (bhit.InBandPanel) ? bhit.Band : null;
            m_GridControlMain.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGridViewMainCustomDrawBandHeader(object sender, BandHeaderCustomDrawEventArgs e)
        {
            if ((SelectedBand == null) || (!IsDesignMode)) return;
            if (e.Band != m_SelectedBand) return;
            //A brush to fill the band's background in the normal state
            //Brush brush = new LinearGradientBrush(e.Bounds, Color.Wheat, Color.Chocolate, 70);
            //A brush to fill the background when the band is pressed
            //Brush brushPressed = new LinearGradientBrush(e.Bounds, Color.WhiteSmoke, Color.Gray, 70);
            Rectangle r = e.Bounds;
            //Draw a 3D border
            //ControlPaint.DrawBorder3D(e.Graphics, r, Border3DStyle.SunkenOuter );
            //r.Inflate(-2, -2);
            e.Graphics.FillRectangle(SystemBrushes.Highlight, r);
            /*r.Inflate(-2, 0);
                //Draw the band's caption with a shadowed effect
                e.Appearance.DrawString(e.Cache, e.Band.Caption, new Rectangle(r.X + 1, r.Y + 1,
                  r.Width, r.Height));*/
            //e.Appearance.DrawString(e.Cache, e.Band.Caption, new Rectangle(r.X + 5, r.Y, r.Width, r.Height),SystemBrushes.HighlightText);
            e.Appearance.DrawString(e.Cache, e.Band.Caption, r, SystemBrushes.HighlightText);
            //Prevent default painting
            e.Handled = true;
            //System.Diagnostics.Trace.Write(e.Band);
        }

        /// <summary>
        /// Определяет, загружен ли предустановленный боковик
        /// </summary>
        /// <returns></returns>
        public bool IsStubLoaded()
        {
            bool result = false;

            var sectionTemplateRow = Tag as FlexibleFormsDS.SectionTemplateRow;
            if (sectionTemplateRow != null)
            {
                if (!sectionTemplateRow.IsIsStubLoadedNull())
                    result = sectionTemplateRow.IsStubLoaded;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGridViewMainMouseDown(object sender, MouseEventArgs e)
        {
            CalculateActive(m_GridControlMain, e.Location);
        }

        internal SimpleButton btnCopy { get; set; }
        internal SimpleButton btnRemove { get; set; }

        private void InitializeComponent()
        {
            this.m_GridControlMain = new DevExpress.XtraGrid.GridControl();
            this.GridViewMain = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.m_RepositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.btnCopy = new SimpleButton();
            this.btnRemove = new SimpleButton();

            this.LabelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_GridControlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_RepositoryItemTextEdit1)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.barManagerRows)).BeginInit();
            this.SuspendLayout();

            

            // 
            // pContainer
            // 
            this.LabelControl.Controls.Add(this.m_GridControlMain);
            this.LabelControl.Size = new System.Drawing.Size(390, 372);
            //Buttons

            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Text = BvMessages.Get("strCopy_Id");
            this.btnRemove.Text = BvMessages.Get("strRemove_Id"); ;
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Left = btnCopy.Left + btnCopy.Width;
            this.btnCopy.Click += OnBtnCopyClick;
            this.btnRemove.Click += OnBtnRemoveClick;
            this.Toolbar.Controls.Add(this.btnCopy);
            this.Toolbar.Controls.Add(this.btnRemove);
            
            // 
            // gridControlMain
            // 
            this.m_GridControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_GridControlMain.Location = new System.Drawing.Point(0, 0);
            this.m_GridControlMain.MainView = this.GridViewMain;
            this.m_GridControlMain.Name = "m_GridControlMain";
            this.m_GridControlMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.m_RepositoryItemTextEdit1});
            this.m_GridControlMain.Size = new System.Drawing.Size(390, 372);
            this.m_GridControlMain.TabIndex = 0;
            this.m_GridControlMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridViewMain});
            // 
            // gridViewMain
            // 
            this.GridViewMain.ColumnPanelRowHeight = 0;
            this.GridViewMain.GridControl = this.m_GridControlMain;
            this.GridViewMain.Name = "gridViewMain";
            this.GridViewMain.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.GridViewMain.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.GridViewMain.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.GridViewMain.OptionsView.ShowColumnHeaders = false;
            this.GridViewMain.OptionsView.ShowGroupPanel = false;
            this.GridViewMain.BandWidthChanged += new DevExpress.XtraGrid.Views.BandedGrid.BandEventHandler(this.OnGridViewMainBandWidthChanged);
            this.GridViewMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridViewMainMouseDown);
            this.GridViewMain.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.OnGridViewMainDragObjectDrop);
            this.GridViewMain.CustomDrawBandHeader += new DevExpress.XtraGrid.Views.BandedGrid.BandHeaderCustomDrawEventHandler(this.OnGridViewMainCustomDrawBandHeader);
            // 
            // repositoryItemTextEdit1
            // 
            this.m_RepositoryItemTextEdit1.AutoHeight = false;
            this.m_RepositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // SectionTable
            // 
            this.Name = "SectionTable";
            this.Size = new System.Drawing.Size(400, 400);
            //this.Controls.Add(this.barDockControlLeft);
            //this.Controls.Add(this.barDockControlRight);
            //this.Controls.Add(this.barDockControlBottom);
            //this.Controls.Add(this.barDockControlTop);
            this.LabelControl.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)(this.m_GridControlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_RepositoryItemTextEdit1)).EndInit();
            
            this.ResumeLayout(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void CellValueChangedDelegate(object sender, CellValueChangedEventArgs e);

        /// <summary>
        /// 
        /// </summary>
        public event CellValueChangedDelegate CellValueChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnBtnRemoveClick(object sender, EventArgs e)
        {
            if (!IdfsSection.HasValue) return;
            //реально строку удаляет презентер
            OnRemoveRow(IdfsSection.Value, GridViewMain.FocusedRowHandle);
            GridViewMain.DeleteRow(GridViewMain.FocusedRowHandle);

            //var row = GridViewMain.GetFocusedDataRow();
            //foreach (BandedGridColumn column in GridViewMain.Columns)
            //{
            //    var band = column.Tag as GridBand;
            //    if (band == null) continue;
            //    var parameterTemplate = band.Tag as FlexibleFormsDS.ParameterTemplateRow;
            //    if (parameterTemplate == null) continue;
            //    //OnDeleteValue(parameterTemplate.idfsParameter, )
            //    row[column.Name] = DBNull.Value;
            //    var arg = new CellValueChangedEventArgs(GridViewMain.FocusedRowHandle, column, String.Empty);
            //    if (CellValueChanged != null) CellValueChanged(GridViewMain, arg);
            //}
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnBtnCopyClick(object sender, EventArgs e)
        {
            var selectedRow = GridViewMain.GetFocusedDataRow();
            if (selectedRow == null) return;
            var newRow = TblUserData.NewRow();
            TblUserData.Rows.Add(newRow);
            foreach (BandedGridColumn column in GridViewMain.Columns)
            {
                var cname = column.Name;
                var band = column.Tag as GridBand;
                if (band == null) continue;
                //var parameterTemplate = band.Tag as FlexibleFormsDS.ParameterTemplateRow;
                //if (parameterTemplate == null) continue;
                newRow[cname] = selectedRow[cname];
                //при создании новой строки используется такой
                var args = new CellValueChangedEventArgs(TblUserData.Rows.Count - 1, column, newRow[cname]);
                //отыскиваем контрол, через который изменяется значение в этом столбце, чтобы имитировать ручной ввод
                OnGridViewMainCellValueChanged(GridViewMain, args);
            }
        }

        /// <summary>
        /// Перетаскивание бэндов в гриде
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGridViewMainDragObjectDrop(object sender, DragObjectDropEventArgs e)
        {
            if (e.Canceled) return;
            //запускаем процедуру обновления индексов в данной табличной секции
            var topView = (sender as AdvBandedGridView);
            if (topView == null) return;
            ReCalculateColumnOrder(topView.Bands);
        }

        /// <summary>
        /// Пересчитывает intOrder у секций и параметров, входящих в табличную секцию
        /// это не нужно делать во время открытия шаблона, но при редактировании нужно
        /// </summary>
        /// <param name="bandCollection"></param>
        private void ReCalculateColumnOrder(GridBandCollection bandCollection)
        {
            if (NowLoading || ParentRender.NowLoadingTemplate || ParentRender.NowClearingTemplate) return;
            for(var i = 0; i < bandCollection.Count; i++)
            {
                var gridBand = bandCollection[i];
                if (gridBand.Tag is FlexibleFormsDS.SectionTemplateRow)
                    ((FlexibleFormsDS.SectionTemplateRow) gridBand.Tag).intOrder = i;
                if (gridBand.Tag is FlexibleFormsDS.ParameterTemplateRow)
                    ((FlexibleFormsDS.ParameterTemplateRow)gridBand.Tag).intOrder = i;
                //для этого бэнда тоже запустим расчёт
                ReCalculateColumnOrder(gridBand.Children);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridBand"></param>
        /// <returns></returns>
        private int GetGridBandIntOrder(GridBand gridBand)
        {
            int result = -1;
            if (gridBand.Tag is FlexibleFormsDS.SectionTemplateRow)
                result = ((FlexibleFormsDS.SectionTemplateRow)gridBand.Tag).intOrder;
            if (gridBand.Tag is FlexibleFormsDS.ParameterTemplateRow)
                result = ((FlexibleFormsDS.ParameterTemplateRow)gridBand.Tag).intOrder;
            return result;
        }

        /// <summary>
        /// Вычисляет правильные видимые индексы для элементов бэнда, опираясь на intOrder
        /// </summary>
        /// <param name="bandCollection"></param>
        private void SetBandsVisibleIndex(GridBandCollection bandCollection)
        {
            if (bandCollection.Count < 2) return;

            bool needRecalculate = true;
            while (needRecalculate)
            {
                needRecalculate = false;
                for (int i = 1; i < bandCollection.Count; i++)
                {
                    //сортируем по возрастанию ордеров
                    GridBand gridBandThis = bandCollection[i];
                    GridBand gridBandLast = bandCollection[i - 1];
                    int orderThis = GetGridBandIntOrder(gridBandThis);
                    int orderLast = GetGridBandIntOrder(gridBandLast);
                    //TODO нужно ли где-то боковик отдельно сортировать?
                    //if ((orderThis < 0) || (orderLast < 0)) continue;
                    if (orderThis < orderLast)
                    {
                        bandCollection.MoveTo(i - 1, gridBandThis);
                        needRecalculate = true; //нужно снова пройти по всем элементам
                    }
                }
            }
        }

        /// <summary>
        /// Изменение ширины бэндов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGridViewMainBandWidthChanged(object sender, BandEventArgs e)
        {
            GridBand gridBand = e.Band;
            if (e.Band != null) RefreshBandChildrenWidth(gridBand.ParentBand ?? gridBand);
        }

        /// <summary>
        /// Обновляет ширины бэндов
        /// </summary>
        /// <param name="gridBand"></param>
        private void RefreshBandChildrenWidth(GridBand gridBand)
        {
            var sectionTemplateRow = gridBand.Tag as FlexibleFormsDS.SectionTemplateRow;
            if (sectionTemplateRow != null) sectionTemplateRow.intWidth = gridBand.Width;
            var parameterTemplateRow = gridBand.Tag as FlexibleFormsDS.ParameterTemplateRow;
            if (parameterTemplateRow != null) parameterTemplateRow.intWidth = gridBand.Width;
            foreach (GridBand bandSibling in gridBand.Children)
            {
                RefreshBandChildrenWidth(bandSibling);
            }
        }
    }
}