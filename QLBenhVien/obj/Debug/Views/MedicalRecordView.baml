   M S B A M L   `   `   `  ����E  AQLBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:QLBenhVien.ViewsQLBenhVien.Views  5      A%clr-namespace:QLBenhVien.UserControlDQLBenhVien.UserControlD  5	      (    "QLBenhVien.Views.MedicalRecordView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:QLBenhVien.Views     5      4uc%clr-namespace:QLBenhVien.UserControlD     5	      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5
   	   _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5        $��window��5   	      MaterialDesignBody#���   5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	     MedicalRecordVM � DataContext# [ 5   	   .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   6*   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      6   .+�5    
   #  MaterialDesignThemes.Wpf.Card  $��0��6   $��5 5 5 0q�6+   .��5!      �  � ColumnDefinitions 5"      �� 5#      $
��Auto �6'   �� 5$      $	��200 �6'   �� 5%      $
��Auto �6'   .+�5'      ��   MaterialDesignToolButton#���  6   .��5(      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Magnify=�6.    J� Opacity$	 .56Q�6=   5)      �� 5*      $��1��6   $��8 0 0 0q�5+   "   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$* $Tìm kiếm bằng tên bệnh nhân��5,   "   .  (MaterialDesignThemes.Wpf.TextFieldAssist   DecorationVisibility$ Hidden=�5-   "   $��0q�5.   "   $	��200��5/   "   $��Center=�51   "   ��50   "   �� *
TextSearch+51   "   �� 52      #���  6   	 �� 	IsDefault	 . $��2��55   !   ��54   !   �� *SearchCommand+.��56        $
 Send=�6.   $	 .56Q�6:   $��8 0 0 0q�6H   57      58      59        5;   
   $��1��6   $��5 5 5 0q�6+   .��5<      � .+�5=      �� $��5 5 15 5q�6   
 �� DisplayMemberPath$
 DisplayName��6-   $ Tên bệnh nhân��6�    �� 
IsEditable .  & "MaterialDesignFloatingHintComboBox#��[ 6	  ��6M   �� *	Patient+ �� SelectedItem 6m   �� *SelectedPatient+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6G  �� 5>      $��5 5 15 5q�6   $
 DisplayName��6-   $ Loại bệnh��6�    . #��[ 6�   ��6M   �� *Sick+ 6j   �� *SelectedSick+$ TwoWay=�$ PropertyChanged=�6=  �� 5?      $��5 5 15 5q�6   $
 DisplayName��6-   $ Tên đơn thuốc��6�    . #��[ 6  ��6M   �� *Prescription+ 6r   �� *SelectedPrescription+$ TwoWay=�$ PropertyChanged=�6Q  �� 5@      $��5 5 15 5q�6   $
 DisplayName��6-   $ Nơi điều trị��6�    . #��[ 6
  ��6M   �� *
Location+ 6n   �� *SelectedLocation+$ TwoWay=�$ PropertyChanged=�6H  (  "System.Windows.Controls.DatePicker  5A      $	��100��6V   $��5 5 15 5q�6b   $ 
Ngày vào��6t    ( $MaterialDesignFloatingHintDatePicker#��[ 6�      SelectedDate 6   �� *DateIn+ �� StringFormat$ dd/MM/yy��6�     5B      $	��100��6W   $��5 5 15 5q�6c   $ Ngày ra��6u   #��[ 6�    6   �� *	DateOut+$ dd/MM/yy��6�   5C      5D        5E   
   $��2��6   $��5q�6+   .��5F      � .+�5G      �� $��Thêm��68   $��5q�6G   $	��100��6R   ��6   �� *
AddCommand+6`   �� 5H      $��Sửa��69   $��5q�6G   $	��100��6R   ��6   �� *EditCommand+6`   �� 5I      $
��Xóa��6;   $��5q�6I   $	��100��6T   ��6   �� *DeleteCommand+6b   �� 5J      $��Tạo đơn thuốc mới��6=   $��5q�6Y   $	��160��6d   ��6   �� *CreateDTCommand+6r   5K      5L        5N   
   $��3��6   $��Stretch=�6+   .��5O      �� $
��Auto=�6   .��5P      �� ��6   �� *List+ 68   �� *SelectedItem+$ OneWayToSource=� �� View 5Q      �� 5R      .P�5S      �� $��Tên bệnh nhân��6-    �� DisplayMemberBinding 6D   �� *Patient.DisplayName+6{   �� 5T      $��Loại bệnh��6-    6@   �� *Sick.DisplayName+6t   �� 5U      $��Mã đơn thuốc��6-    6C   �� *Prescription.Id+6v   �� 5V      $��Nơi điều trị��6-    6C   �� *Location.DisplayName+6{   �� 5W      $��
Ngày vào��6-    6?   �� *DateIn+$ 
dd/MM/yyyy��6�   �� 5X      $��Ngày ra��6-    6>   �� *	DateOut+$ 
dd/MM/yyyy��6�   5Y      5[      5\      5]      5^      5_      