   M S B A M L   `   `   `  ����E  ARProPlugin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:RProPlugin
RProPlugin  5   	       RProPlugin.MainWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:RProPlugin     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � 	Resources 5	      -   'RProPlugin.BooleanToVisibilityConverter    BooleanToVisibilityConverter4   .RProPlugin.InverseBooleanToVisibilityConverter ' #InverseBooleanToVisibilityConverter��%:   &	        &	        5   
   6-     5   
   64   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)   �� 5      $
��Auto �6   6,   .+�5   
   c� $��0��6   .@�5      �� -   5        $)��#Редактирование цен��5      $��0,0,5,0q�6K   �� 5      -   5        $1��+Загрузить спецификацию��5      $��0,0,5,0q�6M   �� 5      -   5        $��Расчет цены��5      $��0,0,5,0q�6H   �� 5      -   5        $��
Выход��5      6%   5      �� 5   
   $��1��6   .@�5      �� $+��%Основные компоненты��6   .��5       &   System.Windows.Controls.DataGrid  -   5        $��
MyDataGrid��5          AutoGenerateColumns$ False��5"         CanUserAddRows$ False��5#      ��5!      �� *
Components+   Columns 5$      4  .System.Windows.Controls.DataGridTemplateColumn  5&      ,  &System.Windows.Controls.DataGridColumn   Header$ Выбрать��61      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6B      CellTemplate 5'      �� 5(   "   .R�5)   &   �� $��Center=�5*   /   	 d� 	IsChecked	 5)   /   �� *
IsSelected+
 �� UpdateSourceTrigger$
 PropertyChanged=�5*   /   5+   #   5-      0  *System.Windows.Controls.DataGridTextColumn  5/      $ Имя��6-   $ * 51   -   1  +System.Windows.Controls.DataGridBoundColumn   Binding 50   -   �� *Name+51   -     52      $ 
BOM имя��6-   $ * 54   -    53   -   �� *	BomName+54   -     56      $ Категория��6-   $ * 58   -    57   -   �� *
Category+58   -     5:      $ Цена��61   $ * 6?    5;      �� 5<   "   .R�5=   &   ��  �� Orientation$ 
Horizontal=�61   $��Center=�6J   .+�5>   *   �� ��64   �� *Price+ �� StringFormat$ 
{0:N2} BYN��y�5?   4   �� *
HasPrice+ �� 	Converter# [  �� 5@   *   -   5        $��Добавить цену��5@   1   $	��100��5D   1   $	��5,0q�5E   1   y�5A   1   �� *
HasPrice+# [ 
 � Tag 5C   1   �� *	BomName+5E   1   5F   '   5G   #   5I      5K      5L      �� 5O      $��Ковейры��6   .��5P        -   5        $��CoversDataGrid��5P      $ False��5Q      $ False��5R      5T      5U      �� 5X      $��Ограждения��6   .��5Y        -   5        $��FencesDataGrid��5Y      $ False��5Z      $ False��5[       5\        5^      $ Выбрать��61   $
 Auto 6B    5_      �� 5`   "   .R�5a   &   �� $��Center=�5b   /   	 5a   /   �� *
IsSelected+$
 PropertyChanged=�5b   /   5c   #   5e        5f      $ Наименование��6-   $	 200 6\    6C   �� *Name+6\     5g      $ 
BOM имя��6-   $	 200 6Z    6>   �� *	BOMName+6Z     5h      $' !Высота ограждения��6-   $	 150 6h    6H   �� *FenceHeight+6h     5i      $% Длина ограждения��6-   $	 150 6g    6G   �� *FenceLength+6g     5j      $ Высота стойки��6-   $	 150 6d    6D   �� *StandHeight+6d     5l      $  Цена за высоту��61   $ * 6I    5m      �� 5n   "   .R�5o   &   �� $ 
Horizontal=�61   $��Center=�6J   .+�5p   *   �� ��64   �� *PricePerHeight+$ 
{0:N2} BYN��y�5q   4   �� *
HasPrice+# [  �� 5r   *   $��Нет цены��64   y�5s   4   �� *
HasPrice+# [ 5t   '   5u   #   5w        5y      $  Цена за ширину��61   $ * 6I    5z      �� 5{   "   .R�5|   &   �� $ 
Horizontal=�61   $��Center=�6J   .+�5}   *   �� ��64   �� *PricePerWidth+$ 
{0:N2} BYN��y�5~   4   �� *
HasPrice+# [  �� 5   *   -	   5        $��Добавить цену��5   1   $	��100��5�   1   $	��5,0q�5�   1   y�5�   1   �� *
HasPrice+# [  5�   1   �� *	BOMName+5�   1   5�   '   5�   #   5�      5�      5�      5�      5�      5�      