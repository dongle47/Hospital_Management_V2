   M S B A M L   `   `   `  ����E  AQLBenhVien, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:QLBenhVien
QLBenhVien  5   	   ` \De.TorstenMandelkow.MetroChart, Version=0.9.5818.20693, Culture=neutral, PublicKeyToken=nullwTclr-namespace:De.TorstenMandelkow.MetroChart;assembly=De.TorstenMandelkow.MetroChartDe.TorstenMandelkow.MetroChart 5   	   A%clr-namespace:QLBenhVien.UserControlDQLBenhVien.UserControlD  5
   	       QLBenhVien.StatisWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:QLBenhVien     5   	   fchartTclr-namespace:De.TorstenMandelkow.MetroChart;assembly=De.TorstenMandelkow.MetroChart     5   	   4uc%clr-namespace:QLBenhVien.UserControlD     5
   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5        $��window��5   	      MaterialDesignBody#���   5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	     StatisVM � DataContext# [ 5   	    � Title$ StatisWindow��5   	   $	��450��6   $	��800��6+   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase	   Actions.	 5       6  0System.Windows.Interactivity.InvokeCommandAction  
   Command
 6$   �� *LoadedWindowCommand+   CommandParameter 6L   ��  �� ElementName$ window��5!      .��5$      �  � RowDefinitions 5%   
   �� 5&      $
��auto �6   6,   �� 5'      6   .+�5*   
   � $��0��6   .+�5+      *   $QLBenhVien.UserControlD.ControlBarUC  
 � Tag 6   �� *Title+$ window��6J   5,      � 5.   
   $��1��6    5/      �� 50      6"   �� 51      6"   .+�54      � $��0��6    � ColumnDefinitions 55      �� 56      6)   �� 57      6)   .+�5:      #  MaterialDesignThemes.Wpf.Card  $��0��6&   $��	10 10 5 5q�66   .��5;      � .+�5<      -  'De.TorstenMandelkow.MetroChart.PieChart  .	  (De.TorstenMandelkow.MetroChart.ChartBase 	  ChartSubTitle$ Tính theo số ca mắc��6)    	  
ChartTitle$ Tỉ lệ mắc bệnh��6M    	  Series 5>   "   0
  *De.TorstenMandelkow.MetroChart.ChartSeries
   
  DisplayMember$ DisplayName��64    
  SeriesTitle$
 sick��5?   0    
  ValueMember$ Count��5A   0   ��5@   0   ��  �� Path$ 
StatisSick� �� Mode$ TwoWay=�5A   0   5C      5D      5E        5G      $��1��6&   $��	5 10 10 5q�66   .��5H      � .+�5I        $" Tính theo số bệnh nhân��6)   $ Tỉ lệ BHYT��6P    5K   "   
  $ Status��64   $
 bhyt��5L   0   $ Count��5N   0   ��5M   0   �� $ 
StatisBHYT�$ TwoWay=�5N   0   5P      5Q      5R      5S      � 5U      $��1��6    5V      �� 5W      6)   �� 5X      6)   .+�5[        $��0��6&   $��	10 5 5 10q�66   .��5\      � .+�5]        $" Tính theo số bệnh nhân��6)   $" Tỉ lệ nơi điều trị��6P    5_   "   
  $ DisplayName��64   $ location��5`   0   $ Count��5b   0   ��5a   0   �� $ StatisLocation�$ TwoWay=�5b   0   5d      5e      5f        5h      $��1��6&   $��	5 5 10 10q�66   .��5i      � $��1��6   .+�5j        $2 ,Tính theo số lượng thuốc sử dụng��6)   $/ )Tỉ lệ loại thuốc đã sử dụng��6Z    5l   "   
  $ DisplayName��64   $ medicine��5m   0   $ Count��5o   0   ��5n   0   �� $ StatisMedicine�$ TwoWay=�5o   0   5q      5r      5s      5u      5v      5w      5x      