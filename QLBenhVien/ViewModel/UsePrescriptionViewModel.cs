using DocumentFormat.OpenXml.Packaging;
using QLBenhVien.Model;
using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;

namespace QLBenhVien.ViewModel
{
    class UsePrescriptionViewModel : BaseViewModel
    {
        private ObservableCollection<QuantityMedicine> _List;
        public ObservableCollection<QuantityMedicine> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Medicine> _Medicine;
        public ObservableCollection<Medicine> Medicine { get => _Medicine; set { _Medicine = value; OnPropertyChanged(); } }

        private int _IdPrescription = Global.globalId;
        public int IdPrescription { get => _IdPrescription; set { _IdPrescription = value; OnPropertyChanged(); } }

        public ICommand LoadedWindowCommand { get; set; }
        public ICommand ExportCommand { get; set; }

        public UsePrescriptionViewModel()
        {
            Medicine = new ObservableCollection<Medicine>(DataProvider.Ins.DB.Medicines);
            List = new ObservableCollection<QuantityMedicine>(DataProvider.Ins.DB.QuantityMedicines.Where(x => x.IdPrescription == Global.globalId));

            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                List = new ObservableCollection<QuantityMedicine>(DataProvider.Ins.DB.QuantityMedicines.Where(x => x.IdPrescription == Global.globalId));
            }
            );

            ExportCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                DataTable table = new DataTable();
                table.Columns.Add("Name", typeof(String));
                table.Columns.Add("Quantity", typeof(int));
                table.Columns.Add("Use", typeof(String));

                foreach (var item in List)
                {
                    table.Rows.Add(item.Medicine.DisplayName, item.Quantity, item.Medicine.Description);
                }

                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.DefaultExt = "*.docx";
                saveFile.Filter = "DOCX files(*.docx|*.docx";

                if (saveFile.ShowDialog() == DialogResult.OK && saveFile.FileName.Length > 0)
                {
                    exportData(table, saveFile.FileName);
                    System.Windows.Forms.MessageBox.Show("Đã xuất file", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            );

        }

        public void exportData(DataTable table, String fileName)
        {
            if (table.Rows.Count != 0)
            {
                int rowCount = table.Rows.Count;
                int columnCount = table.Columns.Count;
                Object[,] dataArray = new object[rowCount + 1, columnCount + 1];
                //add row
                int r = 0;
                for (int c = 0; c < columnCount; c++)
                {
                    for (r = 0; r < rowCount; r++)
                    {
                        dataArray[r, c] = table.Rows[r][c];
                    }
                }
              
                //fileword
                Microsoft.Office.Interop.Word.Document oDoc = new Microsoft.Office.Interop.Word.Document();
                //cho phep xem qua trinh hinh thanh va xuat file; true la xem; false la ko xem
                oDoc.Application.Visible = false;
                //chinh chieu xoay cua van ban
                oDoc.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientLandscape;
                dynamic oRange = oDoc.Content.Application.Selection.Range;
                string oTemp = "";
                for (r = 0; r < rowCount; r++)
                {
                    for (int c = 0; c < columnCount; c++)
                    {
                        oTemp = oTemp + dataArray[r, c] + "\t";
                    }
                }
                //table format
                oRange.Text = oTemp;
                object seperator = Microsoft.Office.Interop.Word.WdTableFieldSeparator.wdSeparateByTabs;
                object applyBorders = true;
                object autoFit = true;
                object autoFitBehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitContent;
                oRange.ConvertToTable(ref seperator, ref rowCount, ref columnCount, Type.Missing, Type.Missing,
                    ref applyBorders, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    ref autoFit, ref autoFitBehavior, Type.Missing);
                oRange.Select();
                oDoc.Application.Selection.Tables[1].Select();
                oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.InsertRowsAbove(1);
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                //header row style
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1;
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Tahoma";
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14;
                //add header row manually
                oDoc.Application.Selection.Tables[1].Cell(1, 1).Range.Text = "Tên";
                oDoc.Application.Selection.Tables[1].Cell(1, 2).Range.Text = "Số lượng";
                oDoc.Application.Selection.Tables[1].Cell(1, 3).Range.Text = "Cách dùng";
                //table style
                oDoc.Application.Selection.Tables[1].set_Style("Grid Table 4 - Accent 5");
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                //header text
                foreach (Microsoft.Office.Interop.Word.Section section in oDoc.Application.ActiveDocument.Sections)
                {
                    Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
                    headerRange.Text = "Hướng dẫn sử dụng đơn thuốc";
                    headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                }

                oDoc.SaveAs(fileName);
            }
        }
    }
}
