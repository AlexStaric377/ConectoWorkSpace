   M S B A M L   `   `   `  ����  Conecto® WorkSpace    Main_Window.FancyPopup   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      \ XHardcodet.Wpf.TaskbarNotification, Version=1.0.5.0, Culture=neutral, PublicKeyToken=null)tb http://www.hardcodet.net/taskbar  5      -   5          >�Name$  me��5      $	��215��5      $	��300��5       � 	Resources 5         
RotateIcon g� 	BeginTime �� 
TargetName �� TargetProperty O� KeyTime O� Value �� 	KeySpline��%E  &	        �� 5	   
   .��5
      U� $ 00:00:00X�6,   $ image��6A   $V P(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)�5   ,   .\�5      �� $ 00:00:00��6'   $ 0Q�6:   �� 5      $ 0,0.284,0.39,1��6'   $ 00:00:01.4000000��6B   $	 360Q�6]   5      5      5       � Triggers 5      :� 5   
   	 :� RoutedEvent$ 	 tb:TaskbarIcon.PopupOpened��6   .[�5      �� -   5        
 ��Name$ 
 RotateIcon_BeginStoryboard��5   G   #��[  6   6G   5      .��5      � .+�5   
   �� $��Stretch=�5      $��Stretch=�5      $
��Auto��5      $
��Auto��5      $��5,5,5,5q�5       �� CornerRadius$ 10,10,10,10��5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ 	#FF474747��5       ��5"      �� 5#       �� EndPoint$ 0.5,1&�5$       �� 
StartPoint$ 0.5,0&�5%      .��5&      �  � Color$ 	#FF58C2FF��5'       � Offset$ 0Q�5(      � 5)      $ 	#FFFFFFFF��5*      $ 1Q�5+      5,      5.      �� 5/   
   -   5         ��Name$ image��56      $
��Left=�50      $��	19,10,0,0q�51      $	��Top=�52      $��72��53      $��72��54      $��/Images/Preferences.png��55      $
��Fill=�56       J� RenderTransformOrigin$ 0.5,0.5&�6+   z�57      V� 58      .~�59      ��  �� ScaleX$ 1Q�6%    �� ScaleY$ 1Q�60   �� 5:       �� AngleX$ 0Q�6$    �� AngleY$ 0Q�6/   �� 5;       �� Angle$ 0Q�6&   T� 5<       T� X$ 0Q�6)    T� Y$ 0Q�6/   5=      5?      �� 5@   
   $��107,10,20,0q�5A      $
��Wrap=�5B      $
��Auto��5C      $	��Top=�5D      $��16(�5E      $
��Bold"�5F      ���	#FF575757$��Right=�5G   $   �� 5H      $�This is a fancy Popup...��5I       � Language$ de-ch�5J      5K      �� 5L   
   -   5        $��Right=�5O      $��Bottom=�5P      $��89��5Q      $��29��5R      $��Click me��5S      $��	0,0,20,20q�5T      ���	#FFFFFFFF � ToolTip 5N      ��  �� Path$ ToolTipText���5V      �� 5W      $	 0,1&�5X      $	 0,0&�5Y      .��5Z      � $ 	#FFFFFFFF��5[      $ 1Q�5\      � 5]      $ 	#FFFFD197��5^      $ 0.459Q�5_      � 5`      $ 	#FFFFDBAD��5a      $ 0Q�5b      � 5c      $ 	#FFF18E23��5d      $ 0.508Q�5e      � 5f      $ 	#FFF9D4AC��5g      $ 0.954Q�5h      5i      5k      �� 5l   
   $��19,92,10,49q�5m      $
��Wrap=�5n      �� 5o      $)�#This user control makes use of the ��6   $ de-ch�6=    �� 5p      $��Italic$�6   $
��Bold"�6%   $�PopupOpened ��67   $ de-ch�6K    �� 5q      $z�tattached routed event . Whenever the popup is opened, this attached event fires and triggers the rotation animation.��5r      $ de-ch�5s      5t      �� 5u   
   $��	0,52,20,0q�6   $	��Top=�6'   $
��Auto��6?   $
��Wrap=�6M   $
��Bold"�6a   $��14(�5v      $��Right=�6"   ���	#FF575757�� 5w      $�Clicks: ��6   $ de-ch�6"    �� 5x      .F�5y      �� $
��Auto��6   $
��Auto��6)   $
��Wrap=�5z      ��5y   7   �� $ 
ClickCount� �� ElementName$ me�� �� Mode$ Default=�5z      5{      5|      �� 5}   
   $��16��5~   0   $
��Left=�6;   $��	19,0,0,23q�6V   $��16��6i   $��Bottom=�5      ��5~      �� $ 
IconSource�5      5�      5�      