   M S B A M L   `   `   `  ����G  CProjekat33BW, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Projekat33BW.RadSaPodacimaProjekat33BW.RadSaPodacima  5   	   ,    &Projekat33BW.RadSaPodacima.EditEtiketa   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:Projekat33BW.RadSaPodacima     5   	   :xctk)http://schemas.xceed.com/wpf/xaml/toolkit     5   	   -   5          � Title$  Dodaj etiketu��5	   	   $	��350��6   $��479.032��6,    � 
ResizeMode$ NoResize=�6<    � WindowStartupLocation$ CenterScreen=�6k    J� CommandBindings 5
      )  #System.Windows.Input.CommandBinding  5   
   -   5        Help��   Command$ ApplicationCommands.Help��5   '   6J   .��5      �  � RowDefinitions 5   
   �� 5      $	��60* �6   �� 5      $
��135* �6   �� 5      $	��74* �6   .+�5   
   �� $��Id oznaka: ��6   $
��Left=�6&   $	��Top=�6A   $��	10,24,0,0q�6Y   $	��121��6l   �� 5   
   $��Boja etikete: ��6   $
��Left=�6)   $��	10,50,0,0q�6D   $	��Top=�6W   $	��101��6o   $��2��6{   �� 5   
   $��Opis etikete: ��6   $
��Left=�6)   $��	10,21,0,0q�6D   $	��Top=�6W   $��1��6o   �� 5   
   -   5        $��	IdEtikete��5      {�.  $
��Left=�5   Q   $��23��6l   $��
136,27,0,0q�6x    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��120��6�   ��6$   ��  �� Path$ 	IdEtikete� �� Mode$ TwoWay=�.��6�   	 � ToolTip	 5      �� 5      $
��Wrap=�6   $	��200��60   ��Unosom sa tastature unesi identifikaciju etikete, potrebno je da bude jedinstvena. Klikom van ovog polja, gubi se fokus sa ovog polja. Kliktom na ovo polje se možes ponovo vratiti.5      .��5      5      �� 5    
   -   5        $��OpisEtikete��5       
 �� AcceptsReturn
 . $
��Left=�5!   Z   $��98��6u   $��	10,52,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��272��6�   $��1��6�   $��2��6�   ��6'   �� $ OpisEtikete�$ TwoWay=�.��6�   	 5"      �� 5#      $
��Wrap=�6   $	��200��60   ��Unosom sa tastature potrebno je uneti dodatne informacije o ovoj etiketi. Klikom van ovog polja, gubi se fokus sa ovog polja. Kliktom na ovo polje se možes ponovo vratiti.5&      .��5'      5(      �� 5)   
   -   5         ��Name$ PotvrdiDugme��5)      $	��0.0q�6'   $��Potvrdi��6=   $
��Left=�5*      $��	14,58,0,0q�6,   $��2��6?   $	��Top=�6L   $��75��6d   	 5+      �� 5,      $
��Wrap=�6   $	��200��60   geKlikom na ovo dugme se potvrđuje pravljenje nove etikete sa unetim sadržajem u okviru ovog prozora.5.      50      �� 51   
   -   5        $ OtkaziDugme��51      $	��0.0q�6&   $��Otkaži��6<   $
��Left=�52      $��
181,58,0,0q�6,   $��2��6@   $	��Top=�6M   $��75��6e   	 53      �� 54      $
��Wrap=�6   $	��200��60   mkKlikom na ovo dugme se otkazuje kreiranje nove etikete i vraća se u prozor odakle je otvorena ovaj prozor.57      59      �� 5:   
   -   5        $��	ListaBoja��5:       �� 
IsEditable .  $
��Left=�5;   �   $��
136,55,0,0q�6�   $	��Top=�6�   $	��120��6�   $��2��6�    �� SelectedValue 6   �� $ BojaEtikete�$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=���6s   �� $ 
PaletaBoja�	 5<      �� 5=      $
��Wrap=�6   $	��200��60   ��Pritiskom na ovo dugme, prilaže se lista svih podržanih boja za etiketu.Klikom van ovog dugmeta, gubi se fokus sa ovog polja. Kliktom na ovo polje se možes ponovo otvoriti lista boja.5@      5B      �� 5C   
   -   5        $ Pomoc��5C      $	��0.0q�6    $��Pomoć��6J   $
��Left=�5D      $��
300,20,0,0q�6,   $��2��6@   $	��Top=�6M   $��75��6e   	 5E      �� 5F      $
��Wrap=�6   $	��200��60   ��Ovde se nalazi dodatna pomoć koja može biti korisna u vezi znanja o etiketama, ili celokupna dokumentacija ovog tutoriala(ukoliko postoji). Takođe, pritiskom na dugme F1 na tastaturi možete otvoriti isti prozor koji se dobija klikom na ovo dugme.5I      5K      �� 5L   
   -	   5        $ Tutorial��5L      $	��0.0q�6#   {�.  $��Tutorial��6b   $
��Left=�6v   $��
300,58,0,0q�6�   $��2��6�   $	��Top=�6�   $��75��6�   	 5M      �� 5N      $
��Wrap=�6   $	��200��60   1/Tutorial trenutno nije dostupan za ovaj prozor.5P      5R      5S      5T      