   M S B A M L   `   `   `  ����F  B01 BaseMVVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:NugetPackage.ViewNugetPackage.View  5   	   ?$clr-namespace:NugetPackage.ViewModelNugetPackage.ViewModel  5   	   %    NugetPackage.View.InstalledView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   1localclr-namespace:NugetPackage.View     5   	   ;
viewmodels$clr-namespace:NugetPackage.ViewModel     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5	   	   $	��650��5   	   $	��725��6     � DataContext  5   	   ��    Locator �� Source# [   �� Path$ 	Installed�.��5      � .  (System.Windows.Interactivity.Interaction   Triggers 5   
   /  )System.Windows.Interactivity.EventTrigger  5         	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6(   �� $ SearchInstalledCommand�5       � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��7* �6   �� 5      $��3* �6   .+�5   
   �� -   5        $��	listNuget��5       $��15(�6   $��0��6�   $��	0,10,0,20q�6�   ��61   �� $ InstalledPackage�	 �� SelectedItem	 6_   �� $ NameInstalledPackage� 5        5      $ MouseLeftButtonUp��6!   . 5         6,   �� $ ShowInstalledCommand�5      5       �� 5!   
   -   5        $��version��5!       $��15(�6   
 �� 
IsReadOnly
 . $��
10,10,0,20q�6A   $��0��6�   $��1��6�    �� TextWrapping$
 Wrap=�6�   " �� VerticalScrollBarVisibility$
 Auto=�6�   ��6U   �� $ ResultInstalledPackage� �� Mode$ OneWay=���6�   Z� �� 5"   
   -   5        $��delete��5"   0   $��Delete��6   $��10(�6"   $��10,0,0,0q�6>   $��Bottom=�6P   $��0��6�   $��1��6�   $��25��6  ���	#FFA6A6A6
���   ���6k   �� $ DeleteCommand���6�   ��  �� RelativeSource �� *FindAncestor+ �� AncestorType$ WindowL���61  Z� 6H  �� 5#   
   -   5        $��logArea��5#       $��15(�6   $��2��6/   $��2��6<   $��0,5,0,5q�6P   
 . $
 Wrap=�6�   $ Hidden=�6�   {�.  ��6s   �� $ 	ResultLog�$ OneWay=���6%  Z� �� 5$   
   -   5        $��	createPDF��5$   6   $��Generate PDF��6   $��10(�6(   $��Bottom=�6G   $��0��6�   $��25��6�   ���	#FFA6A6A6
���   ���6b   �� $ GenerateCommand���6�   ��  �� *FindAncestor+$ WindowL���6  Z� 61  5%      5&      