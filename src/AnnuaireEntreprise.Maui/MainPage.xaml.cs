﻿// Author: Salar
// Created: 06/01/2025
using AnnuaireEntreprise.Maui.ViewModels;


namespace AnnuaireEntreprise.Maui;

public partial class MainPage : ContentPage
{
	// This method is usually generated by the XAML compiler
	public MainPage()
	{
		InitializeComponent();

		// Set the BindingContext for data binding
		BindingContext = new MainPageViewModel();
	}

}

