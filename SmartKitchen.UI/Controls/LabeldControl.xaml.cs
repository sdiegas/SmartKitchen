﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Hsr.CloudSolutions.SmartKitchen.UI.Controls
{
    /// <summary>
    /// Interaction logic for LabeldControl.xaml
    /// </summary>
    [ContentProperty(nameof(ControlContent))]
    public partial class LabeldControl : UserControl
    {
        public LabeldControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
            "Label", typeof (string), typeof (LabeldControl), new PropertyMetadata(default(string)));

        public string Label
        {
            get { return (string) GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty ControlContentProperty = DependencyProperty.Register(
            "ControlContent", typeof (object), typeof (LabeldControl), new PropertyMetadata(default(object)));

        public object ControlContent
        {
            get { return (object) GetValue(ControlContentProperty); }
            set { SetValue(ControlContentProperty, value); }
        }
    }
}
