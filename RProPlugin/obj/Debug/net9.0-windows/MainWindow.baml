   M S B A M L   `   `   `  ����E  ARProPlugin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:RProPlugin
RProPlugin  5   	       RProPlugin.MainWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:RProPlugin     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � 	Resources 5	      -   'RProPlugin.BooleanToVisibilityConverter    BooleanToVisibilityConverter4   .RProPlugin.InverseBooleanToVisibilityConverter ' #InverseBooleanToVisibilityConverter��%:   &	        &	        5   
   6-     5   
   64   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)   �� 5      $
��Auto �6   6,   .+�5   
   c� $��0��6   .@�5      �� -   5        $)��#Редактирование цен��5      $��0,0,5,0q�6K   �� 5      -   5        $1��+Загрузить спецификацию��5      $��0,0,5,0q�6M   �� 5      -   5        $��Расчет цены��5      $��0,0,5,0q�6H   �� 5      -   5        $��
Выход��5      6%   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��
MyDataGrid��5      $��1��5         AutoGenerateColumns$ False��5         CanUserAddRows$ False��5      ��5      �� *
Components+   Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5!      ,  &System.Windows.Controls.DataGridColumn   Header$ Выбрать��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6:      CellTemplate 5"      �� 5#      .R�5$      �� $��Center=�5%   '   	 d� 	IsChecked	 5$   '   �� *
IsSelected+
 �� UpdateSourceTrigger$
 PropertyChanged=�5%   '   5&      5(      0  *System.Windows.Controls.DataGridTextColumn  5*      $ Имя��6%   $ * 5,   %   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5+   %   �� *Name+5,   %     5-      $ 
BOM имя��6%   $ * 5/   %    5.   %   �� *	BomName+5/   %     51      $ Категория��6%   $ * 53   %    52   %   �� *
Category+53   %     55      $ Цена��6)   $ * 67    56      �� 57      .R�58      ��  �� Orientation$ 
Horizontal=�6)   $��Center=�6B   .+�59   "   �� ��6,   �� *Price+ �� StringFormat$ 
{0:N2} BYN��y�5:   ,   �� *
HasPrice+ �� 	Converter# [  �� 5;   "   -   5        $��Добавить цену��5;   )   $	��100��5?   )   $	��5,0q�5@   )   y�5<   )   �� *
HasPrice+# [ 
 � Tag 5>   )   �� *	BomName+5@   )   5A      5B      5D      5F      5G      5H      