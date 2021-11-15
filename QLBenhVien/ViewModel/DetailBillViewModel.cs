using QLBenhVien.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace QLBenhVien.ViewModel
{
    class DetailBillViewModel:BaseViewModel
    {
        private ObservableCollection<QuantityMedicine> _QuantityMedicine;
        public ObservableCollection<QuantityMedicine> QuantityMedicine { get => _QuantityMedicine; set { _QuantityMedicine = value; OnPropertyChanged(); } }

        private ObservableCollection<Medicine> _Medicine;
        public ObservableCollection<Medicine> Medicine { get => _Medicine; set { _Medicine = value; OnPropertyChanged(); } }

        private int _IdMedicalRecord = Global.globalId;
        public int IdMedicalRecord { get => _IdMedicalRecord; set { _IdMedicalRecord = value; OnPropertyChanged(); } }

        private string _NamePatient = Global.globalText;
        public string NamePatient { get => _NamePatient; set { _NamePatient = value; OnPropertyChanged(); } }

        private decimal _TotalPricePrescription;
        public decimal TotalPricePrescription { get => _TotalPricePrescription; set { _TotalPricePrescription = value; OnPropertyChanged(); } }

        private String _NameLocation;
        public String NameLocation { get => _NameLocation; set { _NameLocation = value; OnPropertyChanged(); } }

        private String _TotalDayLocation;
        public String TotalDayLocation { get => _TotalDayLocation; set { _TotalDayLocation = value; OnPropertyChanged(); } }

        private String _TotalPriceLocation;
        public String TotalPriceLocation { get => _TotalPriceLocation; set { _TotalPriceLocation = value; OnPropertyChanged(); } }

        private String _TotalHospitalFee;
        public String TotalHospitalFee { get => _TotalHospitalFee; set { _TotalHospitalFee = value; OnPropertyChanged(); } }

        private String _ReductionPercent;
        public String ReductionPercent { get => _ReductionPercent; set { _ReductionPercent = value; OnPropertyChanged(); } }

        private String _ReductionPrice;
        public String ReductionPrice { get => _ReductionPrice; set { _ReductionPrice = value; OnPropertyChanged(); } }

        private String _FinalFee;
        public String FinalFee { get => _FinalFee; set { _FinalFee = value; OnPropertyChanged(); } }

        public ICommand LoadedWindowCommand { get; set; }
        public ICommand ExportCommand { get; set; }

        public DetailBillViewModel()
        {
            //var IdPrescription = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == IdMedicalRecord).Select(x => x.IdPrescription).SingleOrDefault();
            //QuantityMedicine = new ObservableCollection<QuantityMedicine>(DataProvider.Ins.DB.QuantityMedicines.Where(x => x.IdPrescription == IdPrescription));
            //Medicine = new ObservableCollection<Medicine>(DataProvider.Ins.DB.Medicines);

            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                IdMedicalRecord = Global.globalId;
                NamePatient = Global.globalText;
                var IdPrescription = DataProvider.Ins.DB.MedicalRecords.Where(x => x.Id == IdMedicalRecord).Select(x => x.IdPrescription).SingleOrDefault();
                QuantityMedicine = new ObservableCollection<QuantityMedicine>(DataProvider.Ins.DB.QuantityMedicines.Where(x => x.IdPrescription == IdPrescription));
            }
            );


            ExportCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                DataTable table = new DataTable();
                table.Columns.Add("Info", typeof(String));
                table.Columns.Add("Price", typeof(String));

                table.Rows.Add("Đơn thuốc");
                foreach (var item in QuantityMedicine)
                {
                    table.Rows.Add("        Số lượng: " + item.Quantity.ToString()+"      " +item.Medicine.DisplayName, item.Price.ToString());
                }
                table.Rows.Add("Tổng tiền đơn thuốc", TotalPricePrescription.ToString());
                table.Rows.Add("", "");

                String row2 = "Nơi điều trị: " + NameLocation.ToString() + "      Số ngày ở: " + TotalDayLocation.ToString();
                table.Rows.Add(row2, TotalHospitalFee.ToString());
                table.Rows.Add("", "");
                table.Rows.Add("Phần trăm BHYT: "+ReductionPercent.ToString()+"%", ReductionPrice.ToString());
                table.Rows.Add("Số tiền giảm", ReductionPrice.ToString());
                table.Rows.Add("", "");
                table.Rows.Add("Tổng số tiền phải trả", FinalFee.ToString());

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
                oDoc.Application.Selection.Tables[1].Cell(1, 1).Range.Text = "Thông tin";
                oDoc.Application.Selection.Tables[1].Cell(1, 2).Range.Text = "Giá tiền";

                //table style
                oDoc.Application.Selection.Tables[1].set_Style("Grid Table 4 - Accent 5");
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                
                //header text
                foreach (Microsoft.Office.Interop.Word.Section section in oDoc.Application.ActiveDocument.Sections)
                {
                    Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
                    headerRange.Text = "Chi tiết hóa đơn\n\n"+"Mã bệnh án: "+IdMedicalRecord.ToString() +
                        "\nTên bệnh nhân: "+NamePatient.ToString()+"\n";
                    headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                }

                oDoc.SaveAs(fileName);
            }
        }

    }
}
