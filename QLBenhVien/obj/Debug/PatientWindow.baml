   M S B A M L   `   `   `  ����E  AQLBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:QLBenhVien
QLBenhVien  5   	   A%clr-namespace:QLBenhVien.UserControlDQLBenhVien.UserControlD  5	   	       QLBenhVien.PatientWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:QLBenhVien     5   	   4uc%clr-namespace:QLBenhVien.UserControlD     5	   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5
   	   _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5        $��window��5   	      MaterialDesignBody#���   5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	     	PatientVM � DataContext# [ 5   	    � Title$ Bệnh nhân��5   	   $	��450��6   $	��800��6(   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase	   Actions.	 5      6  0System.Windows.Interactivity.InvokeCommandAction  
   Command
 6$   �� *LoadedWindowCommand+   CommandParameter 6L   ��  �� ElementName$ window��5      .��5"      �  � RowDefinitions 5#   
   �� 5$      $
��auto �6   6,   �� 5%      6   .+�5'   
   � $��0��6   .+�5(      *   $QLBenhVien.UserControlD.ControlBarUC  
 � Tag 6   �� *Title+$ window��6J   5)      � 5+   
   $��1��6    5-      �� 5.      $��50 �6    6.   �� 5/      $
��auto �6    60   �� 50      $
��auto �6    60   �� 51      6"   .+�54      #  MaterialDesignThemes.Wpf.Card  $��0��6"   $��5 5 5 0q�6/   .��55      �  � ColumnDefinitions 56      �� 57      $
��Auto �6+   �� 58      $	��200 �6+   �� 59      $
��Auto �6+   .+�5;      ��   MaterialDesignToolButton#���  6   .��5<      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Magnify=�62    J� Opacity$	 .56Q�6A   5=      �� 5>      $��1��6   $��8 0 0 0q�5?      )	  #MaterialDesignThemes.Wpf.HintAssist 	  Hint$* $Tìm kiếm bằng tên bệnh nhân��5@      .
  (MaterialDesignThemes.Wpf.TextFieldAssist 
  DecorationVisibility$ Hidden=�5A      $��0q�5B      $	��200��5C      $��Center=�5E      ��5D      �� *
TextSearch+5E      �� 5F      #���  6    �� 	IsDefault . $��2��5I      ��5H      �� *SearchCommand+.��5J        $
 Send=�62   $	 .56Q�6>   $��8 0 0 0q�6L   5K      5L      5M        5O      $��1��6"   $��5 5 5 0q�6/   .��5P      � .+�5Q      �� $��5 5 15 5q�6`   $ Họ và tên��6r    % !MaterialDesignFloatingHintTextBox#��[ 6�   ��6   �� *DisplayName+ �� UpdateSourceTrigger$ PropertyChanged=�6�   (  "System.Windows.Controls.DatePicker  5R      $	��100��6_   $��5 5 15 5q�6k   $ 
Ngày sinh��6}    ( $MaterialDesignFloatingHintDatePicker#��[ 6�      SelectedDate 6!   �� *DateOfBirth+ �� StringFormat$ dd/MM/yy��6�   �� 5S      $��5 5 15 5q�6\   $ Địa chỉ��6n   #��[ 6�   ��6   �� *	Address+$ PropertyChanged=�6�   �� 5T      $��5 5 15 5q�6Z   $ Điện thoại��6l   #��[ 6�   ��6   �� *Phone+$ PropertyChanged=�6�   �� 5U      $��25��6   $��	5 10 15 0q�5V      $ Giới tính��5X       �� 
IsEditable .  	  HintOpacity$	 .26Q�5Z       �� Text 5W      �� *Sex+.@�5[      �� $	��Nam��6'   �� 5\      $
��Nữ��6'   5]      �� 5^      $��5 5 15 5q�6]   $ 
Tôn giáo��6o   #��[ 6�   ��6   �� *
Religion+$ PropertyChanged=�6�   5_      5`        5b      $��2��6"   $��5q�6/   .��5c      � .+�5d      �� $��Thêm��6<   $��5q�6K   $	��100��6V   ��6   �� *
AddCommand+6d   �� 5e      $��Sửa��6=   $��5q�6K   $	��100��6V   ��6   �� *EditCommand+6d   �� 5f      $
��Xóa��6?   $��5q�6M   $	��100��6X   ��6   �� *DeleteCommand+6f   5g      5h        5j      $��3��6"   $��0q�6/   $��Stretch=�6:   .��5k      �� $
��Auto=�6   .��5l      �� -   5        $��listPa��5l      ��6-   �� *List+ �� SelectedItem 6J   �� *SelectedItem+ �� Mode$ OneWayToSource=� �� View 5m      �� 5n      .P�5o   "   �� $��Họ và tên��61    �� DisplayMemberBinding 6D   �� *DisplayName+6s   �� 5p   "   $��
Ngày sinh��61    6D   �� *DateOfBirth+$ 
dd/MM/yyyy��6�   �� 5q   "   $��Địa chỉ��61    6B   �� *	Address+6m   �� 5r   "   $��Số điện thoại��61    6H   �� *Phone+6q   �� 5s   "   $��Giới tính��61    6D   �� *Sex+6k   �� 5t   "   $��
Tôn giáo��61    6C   �� *
Religion+6o   5u      5w      5x      5y      5{      5|      5}      