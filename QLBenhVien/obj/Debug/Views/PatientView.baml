   M S B A M L   `   `   `  ����E  AQLBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:QLBenhVien.ViewsQLBenhVien.Views  5      A%clr-namespace:QLBenhVien.UserControlDQLBenhVien.UserControlD  5	      "    QLBenhVien.Views.PatientView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:QLBenhVien.Views     5      4uc%clr-namespace:QLBenhVien.UserControlD     5	      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5
   	   _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5        $��window��5   	      MaterialDesignBody#���   5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	     	PatientVM � DataContext# [ 5   	   .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   6*   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      6   .+�5    
   #  MaterialDesignThemes.Wpf.Card  $��0��6   $��5 5 5 0q�6+   .��5!      �  � ColumnDefinitions 5"      �� 5#      $
��Auto �6'   �� 5$      $	��200 �6'   �� 5%      $
��Auto �6'   .+�5'      ��   MaterialDesignToolButton#���  6   .��5(      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Magnify=�6.    J� Opacity$	 .56Q�6=   5)      �� 5*      $��1��6   $��8 0 0 0q�5+      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$* $Tìm kiếm bằng tên bệnh nhân��5,      .  (MaterialDesignThemes.Wpf.TextFieldAssist   DecorationVisibility$ Hidden=�5-      $��0q�5.      $	��200��5/      $��Center=�51      ��50      �� *
TextSearch+51      �� 52      #���  6   	 �� 	IsDefault	 . $��2��55      ��54      �� *SearchCommand+.��56        $
 Send=�6.   $	 .56Q�6:   $��8 0 0 0q�6H   57      58      59        5;   
   $��1��6   $��5 5 5 0q�6+   .��5<      � .+�5=      �� $��5 5 15 5q�6\   $ Họ và tên��6n    % !MaterialDesignFloatingHintTextBox#��[ 6�   ��6   �� *DisplayName+
 �� UpdateSourceTrigger$
 PropertyChanged=�6�   (  "System.Windows.Controls.DatePicker  5>      $	��100��6[   $��5 5 15 5q�6g   $ 
Ngày sinh��6y    ( $MaterialDesignFloatingHintDatePicker#��[ 6�      SelectedDate 6   �� *DateOfBirth+ �� StringFormat$ dd/MM/yy��6�   �� 5?      $��5 5 15 5q�6X   $ Địa chỉ��6j   #��[ 6�   ��6   �� *	Address+$
 PropertyChanged=�6�   �� 5@      $��5 5 15 5q�6V   $ Điện thoại��6h   #��[ 6�   ��6   �� *Phone+$
 PropertyChanged=�6�   �� 5A      $��25��6   $��	5 10 15 0q�5B      $ Giới tính��5D       �� 
IsEditable .    HintOpacity$	 .26Q�5F       �� Text 5C      �� *Sex+.@�5G      �� $	��Nam��6#   �� 5H      $
��Nữ��6#   5I      �� 5J      $��5 5 15 5q�6Y   $ 
Tôn giáo��6k   #��[ 6�   ��6   �� *
Religion+$
 PropertyChanged=�6�   5K      5L        5N   
   $��2��6   $��5q�6+   .��5O      � .+�5P      �� $��Thêm��68   $��5q�6G   $	��100��6R   ��6   �� *
AddCommand+6`   �� 5Q      $��Sửa��69   $��5q�6G   $	��100��6R   ��6   �� *EditCommand+6`   �� 5R      $
��Xóa��6;   $��5q�6I   $	��100��6T   ��6   �� *DeleteCommand+6b   5S      5T        5V   
   $��3��6   $��0q�6+   $��Stretch=�66   .��5W      �� $
��Auto=�6   .��5X      �� -   5        $��listPa��5X      ��6)   �� *List+ �� SelectedItem 6F   �� *SelectedItem+ �� Mode$ OneWayToSource=� �� View 5Y      �� 5Z      .P�5[      �� $��Họ và tên��6-    �� DisplayMemberBinding 6@   �� *DisplayName+6o   �� 5\      $��
Ngày sinh��6-    6@   �� *DateOfBirth+$ 
dd/MM/yyyy��6�   �� 5]      $��Địa chỉ��6-    6>   �� *	Address+6i   �� 5^      $��Số điện thoại��6-    6D   �� *Phone+6m   �� 5_      $��Giới tính��6-    6@   �� *Sex+6g   �� 5`      $��
Tôn giáo��6-    6?   �� *
Religion+6k   5a      5c      5d      5e      5f      5g      