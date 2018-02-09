﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Discord_UWP.Controls
{
    public sealed partial class VoiceConnectionControl : UserControl
    {
        public VoiceConnectionControl()
        {
            this.InitializeComponent();
            App.VoiceConnectHandler += App_VoiceConnectHandler;
            App.NavigateToGuildHandler += App_NavigateToGuildHandler;
        }
        string guildid = "";
        private void App_NavigateToGuildHandler(object sender, App.GuildNavigationArgs e)
        {
            if (e.GuildId != guildid && ChannelGrid.Visibility == Visibility.Collapsed)
                ShowChannel.Begin();
            else if (ChannelGrid.Visibility != Visibility.Collapsed)
                HideChannel.Begin();
        }

        public void Show()
        {
            ShowContent.Begin();
        }
        public void Hide()
        {
            HideContent.Begin();
        }
        private void App_VoiceConnectHandler(object sender, App.VoiceConnectArgs e)
        {
            guildid = e.GuildId;
            ChannelName.Text = e.ChannelName;
            GuildName.Text = e.GuildName;
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            App.ConnectToVoice(null, null, "","");
        }

        private async void MiniView_Click(object sender, RoutedEventArgs e)
        {
            bool modeSwitched = await ApplicationView.GetForCurrentView().TryEnterViewModeAsync(ApplicationViewMode.CompactOverlay);
        }

        private void Deafen_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Toggle local deafen
            AudioManager.ChangeDeafStatus(Deafen.IsChecked.Value);
        }

        private void Mute_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Toggle local mute
            App.UpdateLocalMute(!LocalModels.LocalState.VoiceState.SelfMute);
        }

        private void VolumeSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            AudioManager.ChangeVolume(e.NewValue/100);
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (Expanded.Visibility == Visibility.Visible)
                HideExpanded.Begin();
            else
                ShowExpanded.Begin();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.NavigateToGuild(guildid);
        }
    }
}
