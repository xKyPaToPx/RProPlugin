   M S B A M L   `   `   `  ����E  ARProPlugin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:RProPlugin
RProPlugin  5   	   !    RProPlugin.EditPricesWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   *localclr-namespace:RProPlugin     5   	   ] YXceed.Wpf.Toolkit.NET5, Version=4.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	     � Title$  EditPricesWindow��5	   	   $	��450��6"   $	��800��6/   .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��5q�6;   .+�5      (  "Xceed.Wpf.Toolkit.WatermarkTextBox  -   5          Name$ NewNameTextBox��5   $   $	��200��5   #   $��0,0,5,0q�5   #      	Watermark$ Введите BOM имя��5   #     5      -   5        $ NewPriceTextBox��5   $   $	��100��5   #   $��0,0,5,0q�5   #   $ Введите цену��5   #   �� 5      -   5        $��Добавить��5      $	��100��6?   5      &   System.Windows.Controls.DataGrid  5   
   $��1��6      AutoGenerateColumns$ False��5!         CanUserAddRows$ False��5"         CanUserDeleteRows$
 True��5#      ��5       �� *Prices+   Columns 5$      0  *System.Windows.Controls.DataGridTextColumn  5%      ,  &System.Windows.Controls.DataGridColumn	   Header$	 
BOM имя��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 5'   %   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5&   %   �� *	BomName+ �� UpdateSourceTrigger$ PropertyChanged=�5'   %     5(      $	 Цена��6%   $
 * 5*   %    5)   %   �� *Price+$ PropertyChanged=� �� ValidatesOnDataErrors$
 True��   ElementStyle 5+      �� 5,       �� 
TargetType" ��6    .�5-      ��  �� Property � �� �� Value$ Right=�6D   5.         EditingElementStyle 50      �� 51      " ��6    .�52      ��  � ��$ Right=�6K   53      55      57      �� 5:   
   $��2��6   $ 
Horizontal=�6"   $��Right=�6;   $��5q�6W   .+�5;      �� -   5        $��Сохранить��5;      $	��100��6<   $��0,0,5,0q�6H   �� 5<      -   5        $��Отмена��5<      $	��100��6;   5=      5>      5?      