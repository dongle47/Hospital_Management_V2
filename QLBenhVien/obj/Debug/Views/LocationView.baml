   M S B A M L   `   `   `  ����E  AQLBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:QLBenhVien.ViewsQLBenhVien.Views  5      A%clr-namespace:QLBenhVien.UserControlDQLBenhVien.UserControlD  5	      #    QLBenhVien.Views.LocationView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:QLBenhVien.Views     5      4uc%clr-namespace:QLBenhVien.UserControlD     5	      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5
   	   _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5        $��window��5   	      MaterialDesignBody#���   5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	     
LocationVM � DataContext# [ 5   	   .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   6*   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      6   .+�5    
   #  MaterialDesignThemes.Wpf.Card  $��0��6   $��5 5 5 0q�6+   .��5!      �  � ColumnDefinitions 5"      �� 5#      $
��Auto �6'   �� 5$      $	��200 �6'   �� 5%      $
��Auto �6'   .+�5'      ��   MaterialDesignToolButton#���  6   .��5(      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Magnify=�6.    J� Opacity$	 .56Q�6=   5)      �� 5*      $��1��6   $��8 0 0 0q�5+   "   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Tìm kiếm bằng tên��5,   "   .  (MaterialDesignThemes.Wpf.TextFieldAssist   DecorationVisibility$ Hidden=�5-   "   $��0q�5.   "   $	��200��5/   "   $��Center=�51   "   ��50   "   �� *
TextSearch+51   "   �� 52      #���  6   	 �� 	IsDefault	 . $��2��55   !   ��54   !   �� *SearchCommand+.��56        $
 Send=�6.   $	 .56Q�6:   $��8 0 0 0q�6H   57      58      59        5;   
   $��1��6   $��5 5 5 0q�6+   .��5<      � .+�5=      �� $��5q�6i   $ Tên nơi điều trị��6t    % !MaterialDesignFloatingHintTextBox#��[ 6�   ��6   �� *DisplayName+
 �� UpdateSourceTrigger$
 PropertyChanged=� �� Mode$ TwoWay=�6�   �� 5>      $��5q�6c   $ Chi phí��6n   #��[ 6�   ��6   �� *Price+$
 PropertyChanged=�$ TwoWay=�6�   5?      5@        5B   
   $��2��6   $��5q�6+   .��5C      � .+�5D      �� $��Thêm��68   $��5q�6G   $	��100��6R   ��6   �� *
AddCommand+6`   �� 5E      $��Sửa��69   $��5q�6G   $	��100��6R   ��6   �� *EditCommand+6`   �� 5F      $
��Xóa��6;   $��5q�6I   $	��100��6T   ��6   �� *DeleteCommand+6b   5G      5H        5J   
   $��3��6   $��Stretch=�6+   .��5K      �� $
��Auto=�6   .��5L      �� ��6   �� *List+ �� SelectedItem 68   �� *SelectedItem+$ TwoWay=� �� View 5M      �� 5N      .P�5O      �� $��ID��6-    �� DisplayMemberBinding 69   �� *Id+6_   �� 5P      $
��Tên��6-    6:   �� *DisplayName+6i   �� 5Q      $��Chi phí��6-    6>   �� *Price+6g   5R      5T      5U      5V      5W      5X      