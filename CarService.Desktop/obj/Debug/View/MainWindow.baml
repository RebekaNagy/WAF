   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	   M ICarService.Desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:CarService.Desktop.ViewModelCarService.Desktop.ViewModel 5   	   (   "CarService.Desktop.View.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   9system&clr-namespace:System;assembly=mscorlib     5   	   @	viewModel*clr-namespace:CarService.Desktop.ViewModel     5   	     � Title$  Car Service��5   	   $	��400��6   $	��500��6*   $	��450��66   $	��450��6F    � 	Resources 5      ��%    5      .��5	      �  � RowDefinitions 5
   
   �� 5      $��25 �6   �� 5      $��* �6   �� 5      $��35 �6    � ColumnDefinitions 5   
   �� 5      $��* �6   .+�5   
   o� $��0��6   $��2��6   .@�5      m� $
��Menu��6   .@�5      m� $��List reservations��6    m� Command 66   �� *LoadCommand+�� 5      m� 5      $��Logout��6    6+   �� *LogoutCommand+�� 5      m� 5      $
��Exit��6    6)   �� *ExitCommand+5      5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��reservationGrid��5      $��1��6*   $��1��67      AutoGenerateColumns .     
IsReadOnly . $��1��6�   ��6y   �� *Reservations+ �� SelectedItem 6�   �� *SelectedReservation+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$
	 Name��6%   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 63   �� *
ClientName+  5       $
	 Time��6%   
 63   �� *Time+  5!      $	 Category��6%   
 67   �� *Type+  5"      $	 Comment��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 66   
 6@   �� *	Comment+5$      � 5&   
   $��4��6   $��2��6   .+�5'      �� $��Create worksheet��6   $��6,5,0,5q�60   $
��Left=�6�   $	��120��6�   ��6A   �� *CreateWorksheetCommand+��6l   �� *SelectedReservation+{�6�   �� *
IsLoaded+5(      5)      5*      