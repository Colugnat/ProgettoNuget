   M S B A M L   `   `   `  ����F  B01 BaseMVVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:NugetPackage.ViewNugetPackage.View  5      ?$clr-namespace:NugetPackage.ViewModelNugetPackage.ViewModel  5           NugetPackage.View.MainView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:NugetPackage.View     5      ;
viewmodels$clr-namespace:NugetPackage.ViewModel     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5	      $	��650��5
      $	��725��6,     � DataContext  5      ��    Locator �� Source# [   �� Path$ Nuget�.��5      � .  (System.Windows.Interactivity.Interaction   Triggers 5   
   /  )System.Windows.Interactivity.EventTrigger  5         	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6(   �� $ SearchNewsCommand�  5       6(   �� $ CheckDeletedCommand�5       � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��6* �6   �� 5      $��3* �6   .+�5   
   �� -   5        $��search��5   2   $��Search: ��6   $��18(�6$   $
��Left=�6@   $��Center=�6[   $��60��6v   $��24��6�   $��0��6�   $��
70,25,0,23q�6�   �� 5   
   -   5        $��input��5   �   $��15(�6   $��Center=�6    $��30��6�   $��Center=�6�   $	��430��6�   $��0��6�   $��2��6�   
���������6=   �� $ StartSearch�	 �� UpdateSourceTrigger$	 PropertyChanged=�.��6�    5        5      $ KeyUp��6!   . 5         6,   �� $ SearchCommand�5       .��5!   &   5"      �� 5#   
   -   5        $��	listNuget��5#       $��15(�6   $��1��6�   ��61   �� $ ResultSearch�
 �� SelectedItem
 6[   �� $ NamePackage� 5$        5%      $ MouseLeftButtonUp��6!   . 5&         6,   �� $ ShowCommand�5'        5(      $ MouseDoubleClick��6!   . 5)         6,   �� $ SaveFastCommand�5*      5,      �� 5-   
   -   5        $��version��5-       $��15(�6    �� 
IsReadOnly . $��	10,0,0,20q�6A   $��1��6�   $��1��6�    �� TextWrapping$
 Wrap=�6�   " �� VerticalScrollBarVisibility$
 Auto=�6�   ��6T   �� $ ResultPackage� �� Mode$ OneWay=���6�   Z� �� 5.   
   -   5        $
��save��5.   .   $
��Save��6   $��10(�6    $��10,0,0,0q�6:   $��Bottom=�6L   $��1��6�   $��1��6�   $��25��6  ���	#FFA6A6A6
���   ���6g   �� $ SaveCommand���6�   ��  �� RelativeSource �� *FindAncestor+ �� AncestorType$ WindowL���6+  Z� 6B  �� 5/   
   -   5        $��logArea��5/       $��15(�6   $��2��6/   $��2��6<   $��	0,5,300,5q�6P    . $
 Wrap=�6�   $ Hidden=�6�   {�.  ��6u   �� $ 	ResultLog�$ OneWay=���6'  Z� �� 50   
   -   5        $
��news��50   0   $��News: ��6   $��18(�6"   $
��Left=�6<   $	��Top=�6W   $��60��6o   $��24��6z   $��2��6�   $��1��6�   $��70, 10, 0, 0q�6�   �� 51   
   -   5        $��listNews��51       $��15(�6   $��2��6�   $��1��6�   $��70, 40, 0, 5q�6�   ��60   �� $ ResultSearchNews�
 6^   �� $ NamePackage� 52        53      $ MouseLeftButtonUp��6!   . 54         6,   �� $ ShowCommand�55      57      58      59      