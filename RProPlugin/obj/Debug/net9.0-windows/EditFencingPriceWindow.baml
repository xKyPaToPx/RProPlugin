   M S B A M L   `   `   `  ����  
RProPlugin'    !RProPlugin.EditFencingPriceWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$>  8Редактирование цен ограждения��5   	   $	��200��6/   $	��300��6<      DialogWindowStyle#��[  5   	    � WindowStartupLocation$ CenterOwner=�5   	   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $'��!Цена за высоту (BYN):��6   $��0��61   $��0��6>   $��	0,0,10,10q�6N   �� 5   
   -   5         ��Name$ PricePerHeightTextBox��5      $��0��61   $��1��6>   $��0,0,0,10q�6N   �� 5   
   $'��!Цена за ширину (BYN):��6   $��1��61   $��0��6>   $��	0,0,10,10q�6N   �� 5   
   -   5        $ PricePerWidthTextBox��5      $��1��60   $��1��6=   $��0,0,0,10q�6M   �� 5   
   $��2��6   $��1��6"    �� Orientation$ 
Horizontal=�62   $��Right=�6K   .+�5      �� -   5        $��Сохранить��5      $��80��6B   $��0,0,10,0q�6M   �� 5      -   5        $��Отмена��5      $��80��6A   5      5      5      