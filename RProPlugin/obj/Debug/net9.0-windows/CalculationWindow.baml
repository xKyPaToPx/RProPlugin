   M S B A M L   `   `   `  ����  
RProPlugin"    RProPlugin.CalculationWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$%  Расчет стоимости��5   	   $	��180��6"   $	��300��6/      DialogWindowStyle#��[  5   	    � WindowStartupLocation$ CenterOwner=�5   	   .��5      � $��5q�6    � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $/��)Цена разработки ПО (BYN):��6   �� 5   
   -   5         ��Name$ SoftwarePriceTextBox��5      $��1��6   $��0,5,0,10q�6=   $
��0.00��6O   �� 5   
   $��2��6   $9��3Цена системы управления (BYN):��6   �� 5   
   $��3��6    �� Orientation$ 
Horizontal=�6"   .+�5      �� -   5        $ ControlSystemPriceTextBox��5      $	��150��69   $��0,0,10,0q�6E    �� 
IsReadOnly$
 True��6W   �� 5      -   5        $��Выбрать Excel��5      $	��100��6K   5      �� 5   
   $��4��6   $ 
Horizontal=�6"   $
��Left=�6;   $��0,10,0,0q�6V   .+�5      �� -   5        $��Рассчитать��5      $	��100��6B   $��0,0,10,0q�6N   �� 5      -   5        $��Отмена��5      $	��100��6;   5      5       5!      