   M S B A M L   `   `   `  ����E  AQLBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:QLBenhVien
QLBenhVien  5   	   A%clr-namespace:QLBenhVien.UserControlDQLBenhVien.UserControlD  5	   	       QLBenhVien.BillWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:QLBenhVien     5   	   4uc%clr-namespace:QLBenhVien.UserControlD     5	   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5
   	   _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5        $��window��5   	      MaterialDesignBody#���   5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    
 BillVM � DataContext# [ 5   	    � Title$ 
Hóa đơn��5   	   $	��450��6   $	��800��6&   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase	   Actions.	 5      6  0System.Windows.Interactivity.InvokeCommandAction  
   Command
 6$   �� *LoadedWindowCommand+   CommandParameter 6L   ��  �� ElementName$ window��5      .��5"      �  � RowDefinitions 5#   
   �� 5$      $
��auto �6   6,   �� 5%      6   .+�5'   
   � $��0��6   .+�5(      *   $QLBenhVien.UserControlD.ControlBarUC  
 � Tag 6   �� *Title+$ window��6J   5)      � 5+   
   $��1��6    5,      �� 5-      $��50 �6    6.   �� 5.      $
��auto �6    60   �� 5/      6"   .+�52      #  MaterialDesignThemes.Wpf.Card  $��0��6"   $��5 5 5 0q�6/   .��53      �  � ColumnDefinitions 54      �� 55      $
��Auto �6+   �� 56      $	��200 �6+   �� 57      $
��Auto �6+   .+�59      ��   MaterialDesignToolButton#���  6   .��5:      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Magnify=�62    J� Opacity$	 .56Q�6A   5;      �� 5<      $��1��6   $��8 0 0 0q�5=   "   )	  #MaterialDesignThemes.Wpf.HintAssist 	  Hint$' !Tìm kiếm bằng mã bệnh án��5>   "   .
  (MaterialDesignThemes.Wpf.TextFieldAssist 
  DecorationVisibility$ Hidden=�5?   "   $��0q�5@   "   $	��200��5A   "   $��Center=�5C   "   ��5B   "   �� *
TextSearch+5C   "   �� 5D      #���  6    �� 	IsDefault . $��2��5G   !   ��5F   !   �� *SearchCommand+.��5H        $
 Send=�62   $	 .56Q�6>   $��8 0 0 0q�6L   5I      5J      5K        5M      $��1��6"   $��5q�6/   .��5N      � .+�5O      �� $��5q�6g   $ Số tiền đóng��6r    % !MaterialDesignFloatingHintTextBox#��[ 6�   ��6   �� *Money+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�6�   �� 5P      $��Đóng tiền��6=   $��5q�6Q   $	��100��6\   ��6   �� *EditCommand+6j   �� 5Q      $��Xem chi tiết hóa đơn��6C   $��5q�6b   $	��170��6m   ��6   �� *DetailBillCommand+6{   5R      5S        5U      $��2��6"   $��Stretch=�6/   .��5V      �� $
��Auto=�6   .��5W      �� ��6   �� *List+ �� SelectedItem 6<   �� *SelectedItem+$ TwoWay=� �� View 5X      �� 5Y      .P�5Z   "   �� $��Mã bệnh án��61    �� DisplayMemberBinding 6E   �� *IdMR+6m   �� 5[   "   $��Tên bệnh nhân��61    6H   �� *PatientName+6w   �� 5\   "   $��Bảo hiểm y tế��61    6H   �� *
CodeBHYT+6t   �� 5]   "   $��Tổng viện phí��61    6H   �� *
TotalFee+6t   �� 5^   "   $��	Còn nợ��61    6A   �� *Owed+6i   5_      5a      5b      5c      5d      5e      5f      