   M S B A M L   `   `   `  ����B  >Creator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:CreatorCreator  5
   	       Creator.FinalCreator   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5	   	   'localclr-namespace:Creator     5
   	   -   5        
���OO/�  � WindowStartupLocation$  CenterOwner=�5   	    � 
ResizeMode$ NoResize=�5   	    � Title$) #Своя Игра: Редактор��5   	   $	��450��6%   $��707.447��62    � Icon$ icon.ico��6B   .��5      �  � ColumnDefinitions 5   
   �� 5      $��3* �6   �� 5      $��7* �6   .+�5   
   � -   5         �Name$ QuestionGrid��5      $��0��6%   � 5   
   $��1��6   .+�5      �� -   5         ��Name$ QuestionLabel��5         YellowContentStyle#��[  6+   $"��Введите вопрос:��6W   $
��Left=�6q   $��0q�6�   $��	10,10,0,0q�6�   $	��Top=�6�   $y�Hidden=�6�   �� 5      -   5         ��Name$ QuestionTextBox��5      #��[  6/   
�������$
��Left=�6n   $	��143��6�   $��	10,44,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $�� ��6�   $	��Top=�6�   $	��230��6�   $y�Hidden=�6  �� 5      -   5        $ AnswerLabel��5      #��[  6)   $ ��Введите ответ:��6U   $
��Left=�6n   $��0q�6�   $��
10,205,0,0q�6�   $	��Top=�6�   $y�Hidden=�6�   �� 5      -   5        $ AnswerTextBox��5      #��[  6-   
�������$
��Left=�6l   $	��143��6�   $��
10,244,0,0q�6�   $
 Wrap=�6�   $�� ��6�   $	��Top=�6�   $	��230��6�   $y�Hidden=�6  �� 5      -   5        $ 	NameLabel��5      #��[  6'   $+��%Введите имя вопроса:��6S   $
��Left=�6r   $��0q�6�   $��
251,83,0,0q�6�   $	��Top=�6�   $y�Hidden=�6�   �� 5      -   5        $ NameTextBox��5      #��[  6+   
�������$
��Left=�6j   $��65��6�   $��251,122,0,0q�6�   $
 Wrap=�6�   $�� ��6�   $	��Top=�6�   $	��230��6�   $y�Hidden=�6  �� 5      -	   5        	 ��Name$	 
SaveButton��5      #��[  6)   $��Сохранить��6U   $
��Left=�6i   $��294,194,0,0q�6�   $	��Top=�6�   $	��152��6�   $��56��6�   $y�Hidden=�6�   �� 5      -
   5        $	 
ExitButton��5      #��[  6)   $��Вернуться��6U   $
��Left=�6i   $��329,351,0,0q�6�   $	��Top=�6�   $	��152��6�   $��56��6�   6�   5      5      5      