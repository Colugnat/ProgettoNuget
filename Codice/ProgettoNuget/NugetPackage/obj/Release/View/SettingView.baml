   M S B A M L   `   `   `  ���� F  B01 BaseMVVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:NugetPackage.ViewNugetPackage.View  ?$clr-namespace:NugetPackage.ViewModelNugetPackage.ViewModel  #    NugetPackage.View.SettingView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:NugetPackage.View     ;
viewmodels$clr-namespace:NugetPackage.ViewModel     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  $	��650��$	��725��  � DataContext  ��    Locator �� Source# [   �� Path$ Setting�.���  � ColumnDefinitions �� $��* ��� $��* � � RowDefinitions �� $��7* ��� $��3* �.+��� -   $
��path��$��30��{�.  $��15(�$
��Left=�$��Center=� �� TextWrapping$
 Wrap=�$	��620��$��2��
����������� $ 	Directory��� -   $��browse��$��10(�$��	Browse...��$��Right=�$��Center=�$	��100��$��1��$��26�����	#FFA6A6A6
���   �$��0,112,0,114q����� $ BrowseCommand�����  �� RelativeSource �� *FindAncestor+ �� AncestorType$ WindowL���Z� �� -   $��logArea��$��15(�$��1��$��2��$��0,5,0,5q� �� 
IsReadOnly . $
 Wrap=�"	 �� VerticalScrollBarVisibility$	 Hidden=�{�.  ���� $ 	ResultLog�
 �� Mode$
 OneWay=���Z� �� $��*��$��1��$
��Left=�$��258,182,0,0q�$��24(�$	��Top=��� $&�� * Add \NugetPackage is mandatory��$��1��$
��Left=�$��156,428,0,0q�$	��Top=��� $��18��$
��Left=�$��
10,250,0,0q�$	��Top=� d� 	IsChecked �� $ IsFastDownloader�.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ Click��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� $ CheckCommand��� $��Fast downloader��$
��Left=�$��
30,245,0,0q�$	��Top=�