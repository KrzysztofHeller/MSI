   M S B A M L   `   `   `  ����R  NKomiwojażer genetyczny, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Komiwojażer_genetycznyKomiwojażer_genetyczny  5   	   (    "Komiwojażer_genetyczny.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:Komiwojażer_genetyczny     5   	     � Title$  
MainWindow��5   	   $��578.966��6   $��637.258��6-   .��5	      � ���	#FF7380AE$��0,0,2,0q�6"    � ColumnDefinitions 5
   
   �� 5      $
��458* �6   �� 5      $
��169* �6   .+�5   
   �� -   5         ��Name$ 
listamiast��5      $	��110��6&    J� RenderTransformOrigin$ 1.734,1.033&�62   $��155,150,0,42q�6V   $ �� IsSynchronizedWithCurrentItem$
 TrueY�6l   $
��Left=�6�   �� 5   
   -   5         ��Name$
 Text��5       �� TextWrapping$
 Wrap=�6    $	��303��64   $��294,34,0,42q�6@   $
��Left=�6U   $��2��6p   �� 5   
   -   5         ��Name$
 Load��5      $
��Load��6   $
��Left=�6.   $	��Top=�6I   $��65��6a   $��	24,48,0,0q�6l   $ 	0.44,1.65&�6�   �� 5   
   -   5        $ Start��5      $��Start��6    $
��Left=�60   $	��Top=�6K   $��65��6c   $��	24,76,0,0q�6n   $ 	0.093,0.6&�6�   �� 5   
   -   5        $ Iteracjetextbox��5      $
��Left=�6+   $��20��6F   $
 Wrap=�6R   $	��Top=�6f   $��70��6~   $��
116,48,0,0q�6�   ���	#FFF7F7F7$
��5000��6�   $ -0.214,0.55&�6�   �� 5   
   $��ilość iteracji��6   $
��Left=�6)   $	��Top=�6D   $ 2.316,0.923&�6\   $��
116,24,0,0q�6�   $��85��6�   $��24��6�   �� 5   
   -   5        $ ilośćmutacjibox��5      $
��Left=�6+   $��20��6F   $
 Wrap=�6R   $	��Top=�6f   $��70��6~   $��116,102,0,0q�6�   ���	#FFF7F7F7$��15��6�   �� 5   
   $��ilość mutacji��6   $
��Left=�6(   $	��Top=�6C   $ 2.316,0.923&�6[   $��
116,73,0,0q�6   $��85��6�   $��24��6�   �� 5   
   -   5        $ osobnicysestartowi��5      $
��Left=�6.   $��20��6I   $
 Wrap=�6U   $	��Top=�6i   $��64��6�   $��
201,48,0,0q�6�   ���	#FFF7F7F7$	��100��6�   $ -0.214,0.55&�6�   �� 5   
   $
��Left=�6   $
��Wrap=�6/   $��osobnicy��6C   $	��Top=�6S   $��
206,29,0,0q�6k   �� 5   
   -   5        $ krzyżowaniatext��5      $
��Left=�6+   $��20��6F   $
 Wrap=�6R   $	��Top=�6f   $��64��6~   $��201,102,0,0q�6�   ���	#FFF7F7F7$��50��6�   $ -0.214,0.55&�6�   �� 5   
   $
��Left=�6   $
��Wrap=�6/   $��krzyżowania��6C   $	��Top=�6V   $��
201,78,0,0q�6n   �� 5   
   -	   5        $ listboxobszar��5      $
��Left=�6)   $	��100��6D   $	��Top=�6Q   $	��100��6i   $��
24,150,0,0q�6u    �� SelectedIndex$ 0��6�   �� 5   
   -
   5        $ listboxalgorytm��5      $
��Left=�6+   $	��100��6F   $	��Top=�6S   $	��100��6k   $��
24,255,0,0q�6w   $ 0��6�   5      5      