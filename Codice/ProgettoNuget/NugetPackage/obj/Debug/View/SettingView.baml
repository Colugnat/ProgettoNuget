   M S B A M L   `   `   `  ����F  B01 BaseMVVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:NugetPackage.ViewNugetPackage.View  5      ?$clr-namespace:NugetPackage.ViewModelNugetPackage.ViewModel  5      #    NugetPackage.View.SettingView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:NugetPackage.View     5      ;
viewmodels$clr-namespace:NugetPackage.ViewModel     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5	      $	��650��5      $	��725��6     � DataContext  5      ��    Locator �� Source# [   �� Path$ Setting�.��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��7* �6   �� 5      $��3* �6   .+�5   
   �� -   5        $
��path��5   �   $��30��6   {�.  $��15(�60   $
��Left=�6>   $��Center=�6Y    �� TextWrapping$
 Wrap=�6t   $	��620��6�   $��2��6�   
���������6�   �� $ 	Directory�6�   �� 5   
   -   5        $��browse��5   3   $��10(�6   $��	Browse...��6   $��Right=�6A   $��Center=�6]   $	��100��6x   $��1��6
  $��26��6  ���	#FFA6A6A6
���   �$��0,112,0,114q�6g  ��6�   �� $ BrowseCommand���6�   ��  �� RelativeSource �� *FindAncestor+ �� AncestorType$ WindowL���6=  Z� 6g  �� 5   
   -   5        $��logArea��5       $��15(�6   $��1��6/   $��2��6<   $��0,5,0,5q�6P    �� 
IsReadOnly . $
 Wrap=�6�   "	 �� VerticalScrollBarVisibility$	 Hidden=�6�   {�.  ��6s   �� $ 	ResultLog�
 �� Mode$
 OneWay=���6%  Z� �� 5   
   $��*��6   $��1��6   $
��Left=�6,   $��258,182,0,0q�6G   $��24(�6\   $	��Top=�6j   �� 5   
   $&�� * Add \NugetPackage is mandatory��6   $��1��6;   $
��Left=�6K   $��156,428,0,0q�6f   $	��Top=�6{   �� 5   
   $��18��6   $
��Left=�6   $��
10,250,0,0q�69   $	��Top=�6M    d� 	IsChecked 6e   �� $ IsFastDownloader�.  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5         	EventName$ Click��6!   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6,   �� $ CheckCommand�5      5       �� 5!   
   $��Fast downloader��6   $
��Left=�6*   $��
30,245,0,0q�6E   $	��Top=�6Y   5"      5#      