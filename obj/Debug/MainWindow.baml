   M S B A M L   `   `   `  ????A  =mousse, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:moussemousse  5   	   3clr-namespace:mousse.ViewModelmousse.ViewModel  5   	       mousse.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:mousse     5   	   Ced4http://schemas.microsoft.com/expression/2010/drawing     5   	   4	viewmodelclr-namespace:mousse.ViewModel     5   	     ? Title$y  sDesign by Марат Кундрау aka Marlo Grayhat. Support by Алексей Бесхлебный aka SolSteler??5
   	   $	??450??6h   $
??1000??6u   
??????? ? WindowStartupLocation$ CenterScreen=?6?    ? DataContext 5      $  mousse.ViewModel.MainViewModel  5   
   .??5      ? -   5         ?Name$ MainGrid??5       ? ColumnDefinitions 5   
   ?? 5      -   5         ??Name$ MainGrid_LeftBorder??5   *   $??25 ?6   6*   ?? 5      $??* ?6   ?? 5      -   5        $ MainGrid_RightBorder??5   *   $??25 ?6   6*    ? RowDefinitions 5   
   ?? 5      $??25 ?6   ?? 5      $??* ?6   ?? 5      $??25 ?6   .+?5   
   ?? -   5        $??1??5      $??1??6!   61   ? 5   
   -   5        $ 
CanvasGrid??5      $??1??5       $??1??5!      .+?5$      ?? ??6   ?? *Nodes+??5%      ?? 5&      .R?5'      ?? 
????ح?.+?5(      ?? -   5        $??1q?5/   !    ?? CornerRadius$ 30??50   !    ?? Left 5*   !   ?? *XPos+
	 ?? Top	 5+   !   ?? *YPos+??5,   !   ?? *Radius+??5-   !   ?? *Radius+??5.   !   ?? *Fill+

 ? Tag
 51   !   ?? *
NodeNumber+._?57   "   ?? $??35(?58   ,   $??Center=?59   '   $??Center=?5:   '   
?????? ??57   ,   ?? *
NodeNumber+5;   '   5=      5>      5?      5A      ?? 5E      ??6   ?? *Lines+??5F      ?? 5G      .R?5H      ?? .+?5I      ??  J? Opacity$	 0.5Q?5N   #   
??? ? ?$??5??5P   #   	 ?? X1 5I   #   ?? *
X1MainLine+	 ?? Y1 5J   #   ?? *
Y1MainLine+	 ?? X2 5K   #   ?? *
X2MainLine+	 ?? Y2 5L   #   ?? *
Y2MainLine+ ?? StrokeEndLineCap 5M   #   ?? *StrokeEndLineCap+5Q      ?? 5R      
???   ?$	??1.5q?5T   !   $??30??5U   !   $??30??5V   !   
??? ? ? 5X   !   ?? *XLineWeightBorder+	 5Y   !   ?? *YLineWeightBorder+._?5Z   "   ?? $??Center=?5[   %   $??Center=?5\   %   ??5Z   ,   ?? *
WeightText+5\   %   5]      5_      5a      5c      5e      5g      5h      