   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	   M ICarService.Desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:CarService.Desktop.ViewModelCarService.Desktop.ViewModel 5   	   3   -CarService.Desktop.View.WorksheetEditorWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   9system&clr-namespace:System;assembly=mscorlib     5   	   @	viewModel*clr-namespace:CarService.Desktop.ViewModel     5   	     � Title$  Car Service��5   	   $	��600��6   $	��500��6*    � WindowStyle$ 
ToolWindow=�66    � 	Resources 5      ��%    5      .��5	      �  � RowDefinitions 5
   
   �� 5      $��35 �6   �� 5      $��* �6   �� 5      $��35 �6    � ColumnDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   .+�5      �� $��Cost:��6   $��5q�6%   �� 5      $	��157��6   $��5q�5      ��5   #   �� *Costs+ �� SelectedItem 6B   �� *SelectedCostDropDown+��5      �� 5      .R�5      �� ��6$   �� *Name+5      5      �� 5      $��Add Cost��6   $��6,5,0,5q�6(   $	��100��6�   ��69   �� *AddCostCommand+��6\   �� *SelectedCostDropDown+6�   �� 5      $��Delete Cost��6   $��6,5,0,5q�6+   $	��100��6�   ��6<   �� *DeleteCostCommand+��6b   �� *SelectedCostList+6�   5      &   System.Windows.Controls.DataGrid  5!   
   -   5        $��costGrid��5!      $��1��6#   $��1��60      AutoGenerateColumns .     
IsReadOnly . $��1��6�   ��6r   �� *
AddedCosts+ 6�   �� *SelectedCostList+	   Columns	 5"      0  *System.Windows.Controls.DataGridTextColumn  5#      ,  &System.Windows.Controls.DataGridColumn
   Header$

 Name��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 1* 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *Name+6L     5$      $
 Amount��6%   $ 1* 6P    65   �� *Amount+6P   5&      �� 5)   
   $��4��6   $ 
Horizontal=�6"   $��1��6;   $��Right=�6O   .+�5*      �� $��Total cost:��6   $��5q�6+   �� 5+      $��5q�63   ��6   �� *	TotalCost+63   �� 5,      $
��Save��6   $��6,5,0,5q�6$   $	��100��6U   ��65   �� *SaveCommand+6U   5-      5.      5/      