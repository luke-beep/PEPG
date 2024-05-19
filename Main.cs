using System.Text;
using PEPG.Models;
using static PEPG.Helpers.PeHelper;

namespace PEPGG;

public partial class Main : Form
{
    private ListView _generalInformationListView;
    private ListView _dosHeaderInformationListView;
    private ListView _fileHeaderInformationListView;
    private ListView _optionalInformationListView;
    private ListView _sectionInformationListView;
    private ListView _directoryInformationListView;
    private ListView _importInformationListView;
    private ListView _exportInformationListView;
    private ListView _loadConfigInformationListView;
    private ListView _resourceInformationListView;

    public Main()
    {
        InitializeComponent();
        Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath) ?? throw new Exception("Icon not found");
        var path = PromptUserToSelectPeFile();
        if(string.IsNullOrEmpty(path))
        {
            Environment.Exit(1);
        }
        Load += (_, _) =>
        {
            Text = Path.GetFileName(path);

            var (dosHeader, fileHeader, optionalInformation, generalInformation, sectionInformations,
                directoryInformations,
                importInformations, exportInformations, loadConfigInformation) = ProcessPeFile(path);
            InitializeGeneralInformationListView();
            InitializeDosHeaderInformationListView();
            InitializeFileHeaderInformationListView();
            InitializeOptionalInformationListView();
            InitializeSectionInformationListView([
                "Name", "Virtual Size", "Virtual Address", "Size of Raw Data", "Pointer to Raw Data",
                "Pointer to Relocations", "Pointer to Line Numbers", "Number of Relocations", "Number of Line Numbers",
                "Characteristics", "Hash", "Entropy"
            ]);
            InitializeDirectoryInformationListView([
                "Name", "Size", "Virtual Address Range", "Section", "Entropy", "Hash"
            ]);
            InitializeImportInformationListView(
                [
                    "Function Name", "Undecorated Name", "DLL Name", "RVA", "Hint"
                ]
            );
            InitializeExportInformationListView([
                "Function Name", "DLL Name", "Ordinal", "RVA", "Address"
            ]);
            InitializeLoadConfigInformationListView();

            PopulateGeneralInformationListView(generalInformation);
            PopulateDosHeaderInformationListView(dosHeader);
            PopulateFileHeaderInformationListView(fileHeader);
            PopulateOptionalInformationListView(optionalInformation);
            PopulateSectionInformationListView(sectionInformations);
            PopulateDirectoryInformationListView(directoryInformations);
            PopulateImportInformationListView(importInformations);
            PopulateExportInformationListView(exportInformations);
            if (loadConfigInformation != null)
                PopulateLoadConfigInformationListView(loadConfigInformation);
        };
    }

    private void InitializeGeneralInformationListView()
    {
        _generalInformationListView = new ListView
        {
            Dock = DockStyle.Fill,
            View = View.Details,
            Text = @"General Information",
            FullRowSelect = true,
            HeaderStyle = ColumnHeaderStyle.Nonclickable,
            MultiSelect = true
        };

        _generalInformationListView.Columns.Add("Property", -2);
        _generalInformationListView.Columns.Add("Value", -2);
        ContextMenuStrip contextMenu = new();
        ToolStripMenuItem copyMenuItem = new("Copy");
        ToolStripMenuItem copyValueItem = new("Copy Value");
        copyMenuItem.Click += (_, _) =>
        {
            var selectedItems = _generalInformationListView.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            var sb = new StringBuilder();
            foreach (ListViewItem item in selectedItems)
                sb.AppendLine($"{item.SubItems[0].Text}: {item.SubItems[1].Text}");

            Clipboard.SetText(sb.ToString());
        };
        copyValueItem.Click += (_, _) =>
        {
            var selectedItems = _generalInformationListView.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            var sb = new StringBuilder();
            foreach (ListViewItem item in selectedItems) sb.AppendLine(item.SubItems[1].Text);

            Clipboard.SetText(sb.ToString());
        };
        contextMenu.Items.Add(copyMenuItem);
        contextMenu.Items.Add(copyValueItem);

        _generalInformationListView.ContextMenuStrip = contextMenu;


        MainControl.TabPages.Add(new TabPage("General Information") { Controls = { _generalInformationListView } });
    }

    private void InitializeDosHeaderInformationListView()
    {
        _dosHeaderInformationListView = new ListView
        {
            Dock = DockStyle.Fill,
            View = View.Details,
            Text = @"DOS Header Information",
            FullRowSelect = true,
            HeaderStyle = ColumnHeaderStyle.Nonclickable,
            MultiSelect = true
        };

        _dosHeaderInformationListView.Columns.Add("Property", -2);
        _dosHeaderInformationListView.Columns.Add("Value", -2);

        ContextMenuStrip contextMenu = new();
        ToolStripMenuItem copyMenuItem = new("Copy");
        ToolStripMenuItem copyValueItem = new("Copy Value");
        copyMenuItem.Click += (_, _) =>
        {
            var selectedItems = _dosHeaderInformationListView.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            var sb = new StringBuilder();
            foreach (ListViewItem item in selectedItems)
                sb.AppendLine($"{item.SubItems[0].Text}: {item.SubItems[1].Text}");

            Clipboard.SetText(sb.ToString());
        };
        copyValueItem.Click += (_, _) =>
        {
            var selectedItems = _dosHeaderInformationListView.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            var sb = new StringBuilder();
            foreach (ListViewItem item in selectedItems) sb.AppendLine(item.SubItems[1].Text);

            Clipboard.SetText(sb.ToString());
        };
        contextMenu.Items.Add(copyMenuItem);
        contextMenu.Items.Add(copyValueItem);

        _dosHeaderInformationListView.ContextMenuStrip = contextMenu;

        MainControl.TabPages.Add(new TabPage("DOS Header Information")
            { Controls = { _dosHeaderInformationListView } });
    }

    private void InitializeFileHeaderInformationListView()
    {
        _fileHeaderInformationListView = new ListView
        {
            Dock = DockStyle.Fill,
            View = View.Details,
            Text = @"File Header Information",
            FullRowSelect = true,
            HeaderStyle = ColumnHeaderStyle.Nonclickable,
            MultiSelect = true
        };

        _fileHeaderInformationListView.Columns.Add("Property", -2);
        _fileHeaderInformationListView.Columns.Add("Value", -2);

        ContextMenuStrip contextMenu = new();
        ToolStripMenuItem copyMenuItem = new("Copy");
        ToolStripMenuItem copyValueItem = new("Copy Value");
        copyMenuItem.Click += (_, _) =>
        {
            var selectedItems = _fileHeaderInformationListView.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            var sb = new StringBuilder();
            foreach (ListViewItem item in selectedItems)
                sb.AppendLine($"{item.SubItems[0].Text}: {item.SubItems[1].Text}");

            Clipboard.SetText(sb.ToString());
        };
        copyValueItem.Click += (_, _) =>
        {
            var selectedItems = _fileHeaderInformationListView.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            var sb = new StringBuilder();
            foreach (ListViewItem item in selectedItems) sb.AppendLine(item.SubItems[1].Text);

            Clipboard.SetText(sb.ToString());
        };
        contextMenu.Items.Add(copyMenuItem);
        contextMenu.Items.Add(copyValueItem);

        _fileHeaderInformationListView.ContextMenuStrip = contextMenu;

        MainControl.TabPages.Add(new TabPage("File Header Information")
            { Controls = { _fileHeaderInformationListView } });
    }

    private void InitializeOptionalInformationListView()
    {
        _optionalInformationListView = new ListView
        {
            Dock = DockStyle.Fill,
            View = View.Details,
            Text = @"Optional Information",
            FullRowSelect = true,
            HeaderStyle = ColumnHeaderStyle.Nonclickable,
            MultiSelect = true
        };

        _optionalInformationListView.Columns.Add("Property", -2);
        _optionalInformationListView.Columns.Add("Value", -2);

        ContextMenuStrip contextMenu = new();
        ToolStripMenuItem copyMenuItem = new("Copy");
        ToolStripMenuItem copyValueItem = new("Copy Value");
        copyMenuItem.Click += (_, _) =>
        {
            var selectedItems = _optionalInformationListView.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            var sb = new StringBuilder();
            foreach (ListViewItem item in selectedItems)
                sb.AppendLine($"{item.SubItems[0].Text}: {item.SubItems[1].Text}");

            Clipboard.SetText(sb.ToString());
        };
        copyValueItem.Click += (_, _) =>
        {
            var selectedItems = _optionalInformationListView.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            var sb = new StringBuilder();
            foreach (ListViewItem item in selectedItems) sb.AppendLine(item.SubItems[1].Text);

            Clipboard.SetText(sb.ToString());
        };
        contextMenu.Items.Add(copyMenuItem);
        contextMenu.Items.Add(copyValueItem);

        _optionalInformationListView.ContextMenuStrip = contextMenu;

        MainControl.TabPages.Add(new TabPage("Optional Information")
            { Controls = { _optionalInformationListView } });
    }

    private void InitializeSectionInformationListView(List<string> list)
    {
        _sectionInformationListView = new ListView
        {
            Dock = DockStyle.Fill,
            View = View.Details,
            Text = @"Sections",
            FullRowSelect = true,
            HeaderStyle = ColumnHeaderStyle.Nonclickable,
            MultiSelect = true
        };

        foreach (var t in list) _sectionInformationListView.Columns.Add($"{t}", -2);

        ContextMenuStrip contextMenu = new();
        ToolStripMenuItem copyMenuItem = new("Copy");
        ToolStripMenuItem copyValueItem = new("Copy Value");
        copyMenuItem.Click += (_, _) =>
        {
            var selectedItems = _sectionInformationListView.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            var sb = new StringBuilder();
            foreach (ListViewItem item in selectedItems)
                sb.AppendLine($"{item.SubItems[0].Text}: {item.SubItems[1].Text}");

            Clipboard.SetText(sb.ToString());
        };
        copyValueItem.Click += (_, _) =>
        {
            var selectedItems = _sectionInformationListView.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            var sb = new StringBuilder();
            foreach (ListViewItem item in selectedItems) sb.AppendLine(item.SubItems[1].Text);

            Clipboard.SetText(sb.ToString());
        };
        contextMenu.Items.Add(copyMenuItem);
        contextMenu.Items.Add(copyValueItem);

        _sectionInformationListView.ContextMenuStrip = contextMenu;

        MainControl.TabPages.Add(new TabPage("Sections")
            { Controls = { _sectionInformationListView } });
    }

    private void InitializeDirectoryInformationListView(List<string> list)
    {
        _directoryInformationListView = new ListView
        {
            Dock = DockStyle.Fill,
            View = View.Details,
            Text = @"Directories",
            FullRowSelect = true,
            HeaderStyle = ColumnHeaderStyle.Nonclickable,
            MultiSelect = true
        };

        foreach (var t in list) _directoryInformationListView.Columns.Add($"{t}", -2);

        ContextMenuStrip contextMenu = new();
        ToolStripMenuItem copyMenuItem = new("Copy");
        ToolStripMenuItem copyValueItem = new("Copy Value");
        copyMenuItem.Click += (_, _) =>
        {
            var selectedItems = _directoryInformationListView.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            var sb = new StringBuilder();
            foreach (ListViewItem item in selectedItems)
                sb.AppendLine($"{item.SubItems[0].Text}: {item.SubItems[1].Text}");

            Clipboard.SetText(sb.ToString());
        };
        copyValueItem.Click += (_, _) =>
        {
            var selectedItems = _directoryInformationListView.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            var sb = new StringBuilder();
            foreach (ListViewItem item in selectedItems) sb.AppendLine(item.SubItems[1].Text);

            Clipboard.SetText(sb.ToString());
        };
        contextMenu.Items.Add(copyMenuItem);
        contextMenu.Items.Add(copyValueItem);

        _directoryInformationListView.ContextMenuStrip = contextMenu;

        MainControl.TabPages.Add(new TabPage("Directories")
            { Controls = { _directoryInformationListView } });
    }

    private void InitializeImportInformationListView(List<string> list)
    {
        _importInformationListView = new ListView
        {
            Dock = DockStyle.Fill,
            View = View.Details,
            Text = @"Imports",
            FullRowSelect = true,
            HeaderStyle = ColumnHeaderStyle.Nonclickable,
            MultiSelect = true
        };

        foreach (var t in list) _importInformationListView.Columns.Add($"{t}", -2);

        ContextMenuStrip contextMenu = new();
        ToolStripMenuItem copyMenuItem = new("Copy");
        ToolStripMenuItem copyValueItem = new("Copy Value");
        copyMenuItem.Click += (_, _) =>
        {
            var selectedItems = _importInformationListView.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            var sb = new StringBuilder();
            foreach (ListViewItem item in selectedItems)
                sb.AppendLine($"{item.SubItems[0].Text}: {item.SubItems[1].Text}");

            Clipboard.SetText(sb.ToString());
        };
        copyValueItem.Click += (_, _) =>
        {
            var selectedItems = _importInformationListView.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            var sb = new StringBuilder();
            foreach (ListViewItem item in selectedItems) sb.AppendLine(item.SubItems[1].Text);

            Clipboard.SetText(sb.ToString());
        };
        contextMenu.Items.Add(copyMenuItem);
        contextMenu.Items.Add(copyValueItem);

        _importInformationListView.ContextMenuStrip = contextMenu;

        MainControl.TabPages.Add(new TabPage("Imports")
            { Controls = { _importInformationListView } });
    }

    private void InitializeExportInformationListView(List<string> list)
    {
        _exportInformationListView = new ListView
        {
            Dock = DockStyle.Fill,
            View = View.Details,
            Text = @"Exports",
            FullRowSelect = true,
            HeaderStyle = ColumnHeaderStyle.Nonclickable,
            MultiSelect = true
        };

        foreach (var t in list) _exportInformationListView.Columns.Add($"{t}", -2);

        ContextMenuStrip contextMenu = new();
        ToolStripMenuItem copyMenuItem = new("Copy");
        ToolStripMenuItem copyValueItem = new("Copy Value");
        copyMenuItem.Click += (_, _) =>
        {
            var selectedItems = _exportInformationListView.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            var sb = new StringBuilder();
            foreach (ListViewItem item in selectedItems)
                sb.AppendLine($"{item.SubItems[0].Text}: {item.SubItems[1].Text}");

            Clipboard.SetText(sb.ToString());
        };
        copyValueItem.Click += (_, _) =>
        {
            var selectedItems = _exportInformationListView.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            var sb = new StringBuilder();
            foreach (ListViewItem item in selectedItems) sb.AppendLine(item.SubItems[1].Text);

            Clipboard.SetText(sb.ToString());
        };
        contextMenu.Items.Add(copyMenuItem);
        contextMenu.Items.Add(copyValueItem);

        _exportInformationListView.ContextMenuStrip = contextMenu;

        MainControl.TabPages.Add(new TabPage("Exports")
            { Controls = { _exportInformationListView } });
    }

    private void InitializeLoadConfigInformationListView()
    {
        _loadConfigInformationListView = new ListView
        {
            Dock = DockStyle.Fill,
            View = View.Details,
            Text = @"Load Configuration",
            FullRowSelect = true,
            HeaderStyle = ColumnHeaderStyle.Nonclickable,
            MultiSelect = true
        };

        _loadConfigInformationListView.Columns.Add("Property", -2);
        _loadConfigInformationListView.Columns.Add("Value", -2);

        ContextMenuStrip contextMenu = new();
        ToolStripMenuItem copyMenuItem = new("Copy");
        ToolStripMenuItem copyValueItem = new("Copy Value");
        copyMenuItem.Click += (_, _) =>
        {
            var selectedItems = _loadConfigInformationListView.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            var sb = new StringBuilder();
            foreach (ListViewItem item in selectedItems)
                sb.AppendLine($"{item.SubItems[0].Text}: {item.SubItems[1].Text}");

            Clipboard.SetText(sb.ToString());
        };
        copyValueItem.Click += (_, _) =>
        {
            var selectedItems = _loadConfigInformationListView.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            var sb = new StringBuilder();
            foreach (ListViewItem item in selectedItems) sb.AppendLine(item.SubItems[1].Text);

            Clipboard.SetText(sb.ToString());
        };
        contextMenu.Items.Add(copyMenuItem);
        contextMenu.Items.Add(copyValueItem);

        _loadConfigInformationListView.ContextMenuStrip = contextMenu;

        MainControl.TabPages.Add(new TabPage("Load Configuration")
            { Controls = { _loadConfigInformationListView } });
    }

    private void PopulateGeneralInformationListView(GeneralInformation info)
    {
        _generalInformationListView.Items.Add(new ListViewItem(["Target Machine", info.TargetMachine]));
        _generalInformationListView.Items.Add(new ListViewItem(["Image Name", info.ImageName]));
        _generalInformationListView.Items.Add(new ListViewItem(["Image Path", info.ImagePath]));
        _generalInformationListView.Items.Add(new ListViewItem(["Image Size", $"{info.ImageSize / 1024.0:F1} kB"]));
        _generalInformationListView.Items.Add(new ListViewItem(["Entry Point", $"0x{info.EntryPoint:X}"]));
        _generalInformationListView.Items.Add(new ListViewItem(["Image Base", $"0x{info.ImageBase:X} S"]));
        _generalInformationListView.Items.Add(new ListViewItem(["Image Entropy", $"{info.ImageEntropy:F6}"]));
        _generalInformationListView.Items.Add(new ListViewItem([
            "Time Stamp", $"{info.TimeStamp:hh:mm:ss tt d/M/yyyy}"
        ]));
        _generalInformationListView.Items.Add(new ListViewItem(["Header Checksum", $"0x{info.HeaderChecksum:X}"]));
        _generalInformationListView.Items.Add(new ListViewItem(["Header Spare", info.HeaderSpare.ToString()]));
        _generalInformationListView.Items.Add(new ListViewItem(["Subsystem", info.Subsystem]));
        _generalInformationListView.Items.Add(new ListViewItem([
            "Subsystem Version", info.MajorSubsystemVersion + "." + info.MinorSubsystemVersion
        ]));
        _generalInformationListView.Items.Add(new ListViewItem(["Characteristics", info.Characteristics]));
        _generalInformationListView.Items.Add(new ListViewItem([
            "Creation Date", $"{info.CreationDate:hh:mm:ss tt d/M/yyyy}"
        ]));
        _generalInformationListView.Items.Add(new ListViewItem([
            "Modification Date", $"{info.ModificationDate:hh:mm:ss tt d/M/yyyy}"
        ]));
    }

    private void PopulateDosHeaderInformationListView(DosHeaderInformation dosHeader)
    {
        _dosHeaderInformationListView.Items.Add(new ListViewItem(["Magic", $"0x{dosHeader.Magic:X}"]));
        _dosHeaderInformationListView.Items.Add(new ListViewItem([
            "Bytes on Last Page", $"0x{dosHeader.BytesOnLastPage:X}"
        ]));
        _dosHeaderInformationListView.Items.Add(new ListViewItem(["Pages in File", $"0x{dosHeader.PagesInFile:X}"]));
        _dosHeaderInformationListView.Items.Add(new ListViewItem(["Relocations", $"0x{dosHeader.Relocations:X}"]));
        _dosHeaderInformationListView.Items.Add(new ListViewItem(["Size of Header", $"0x{dosHeader.SizeOfHeader:X}"]));
        _dosHeaderInformationListView.Items.Add(new ListViewItem([
            "Minimum Extra Paragraphs", $"0x{dosHeader.MinimumExtraParagraphs:X}"
        ]));
        _dosHeaderInformationListView.Items.Add(new ListViewItem([
            "Maximum Extra Paragraphs", $"0x{dosHeader.MaximumExtraParagraphs:X}"
        ]));
        _dosHeaderInformationListView.Items.Add(new ListViewItem(["Initial SS", $"0x{dosHeader.InitialSs:X}"]));
        _dosHeaderInformationListView.Items.Add(new ListViewItem(["Initial SP", $"0x{dosHeader.InitialSp:X}"]));
        _dosHeaderInformationListView.Items.Add(new ListViewItem(["Checksum", $"0x{dosHeader.Checksum:X}"]));
        _dosHeaderInformationListView.Items.Add(new ListViewItem(["Initial IP", $"0x{dosHeader.InitialIp:X}"]));
        _dosHeaderInformationListView.Items.Add(new ListViewItem(["Initial CS", $"0x{dosHeader.InitialCs:X}"]));
        _dosHeaderInformationListView.Items.Add(new ListViewItem([
            "Address of Relocation", $"0x{dosHeader.AddressOfRelocationTable:X}"
        ]));
        _dosHeaderInformationListView.Items.Add(new ListViewItem(["Overlay Number", $"0x{dosHeader.OverlayNumber:X}"]));
        _dosHeaderInformationListView.Items.Add(new ListViewItem(["OEM Identifier", $"0x{dosHeader.Oemid:X}"]));
        _dosHeaderInformationListView.Items.Add(new ListViewItem(["OEM Information", $"0x{dosHeader.OemInfo:X}"]));
        _dosHeaderInformationListView.Items.Add(new ListViewItem([
            "Address of New EXE Header", $"0x{dosHeader.AddressOfNewExeHeader:X}"
        ]));
    }

    private void PopulateFileHeaderInformationListView(FileHeaderInformation fileHeader)
    {
        _fileHeaderInformationListView.Items.Add(new ListViewItem(["Signature", $"0x{fileHeader.Signature:X}"]));
        _fileHeaderInformationListView.Items.Add(new ListViewItem(["Machine", $"0x{fileHeader.Machine:X}"]));
        _fileHeaderInformationListView.Items.Add(new ListViewItem([
            "Number of Sections", fileHeader.NumberOfSections.ToString()
        ]));
        _fileHeaderInformationListView.Items.Add(new ListViewItem([
            "Time Date Stamp", $"{fileHeader.TimeDateStamp:hh:mm:ss tt d/M/yyyy}"
        ]));
        _fileHeaderInformationListView.Items.Add(new ListViewItem([
            "Pointer to Symbol Table", $"0x{fileHeader.PointerToSymbolTable:X}"
        ]));
        _fileHeaderInformationListView.Items.Add(new ListViewItem([
            "Number of Symbols", fileHeader.NumberOfSymbols.ToString()
        ]));
        _fileHeaderInformationListView.Items.Add(new ListViewItem([
            "Size of Optional Header", $"0x{fileHeader.SizeOfOptionalHeader:X} ({fileHeader.SizeOfOptionalHeader} B)"
        ]));
        _fileHeaderInformationListView.Items.Add(new ListViewItem([
            "Characteristics", $"0x{fileHeader.Characteristics:X}"
        ]));
    }

    private void PopulateOptionalInformationListView(OptionalInformation optionalHeader)
    {
        _optionalInformationListView.Items.Add(new ListViewItem(["Magic", $"0x{optionalHeader.Magic:X}"]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "Major Linker Version", optionalHeader.MajorLinkerVersion.ToString()
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "Minor Linker Version", optionalHeader.MinorLinkerVersion.ToString()
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "Size of Code", $"0x{optionalHeader.SizeOfCode:X} ({optionalHeader.SizeOfCode / 1024:F1} kB)"
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "Size of Initialized Data",
            $"0x{optionalHeader.SizeOfInitializedData:X} ({optionalHeader.SizeOfInitializedData / 1024:F1} kB)"
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "Size of Uninitialized Data",
            $"0x{optionalHeader.SizeOfUninitializedData:X} ({optionalHeader.SizeOfUninitializedData / 1024:F1} kB)"
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "Address of Entry Point", $"0x{optionalHeader.AddressOfEntryPoint:X}"
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem(["Base of Code", $"0x{optionalHeader.BaseOfCode:X}"]));
        _optionalInformationListView.Items.Add(new ListViewItem(["Image Base", $"0x{optionalHeader.ImageBase:X}"]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "Section Alignment", $"0x{optionalHeader.SectionAlignment:X}"
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "File Alignment", $"0x{optionalHeader.FileAlignment:X}"
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "Major OS Version", optionalHeader.MajorOperatingSystemVersion.ToString()
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "Minor OS Version", optionalHeader.MinorOperatingSystemVersion.ToString()
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "Major Image Version", optionalHeader.MajorImageVersion.ToString()
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "Minor Image Version", optionalHeader.MinorImageVersion.ToString()
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "Major Subsystem Version", optionalHeader.MajorSubsystemVersion.ToString()
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "Minor Subsystem Version", optionalHeader.MinorSubsystemVersion.ToString()
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "Win32 Version Value", $"0x{optionalHeader.Win32VersionValue:X}"
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "Size of Image", $"0x{optionalHeader.SizeOfImage:X} ({optionalHeader.SizeOfImage / 1024:F1} kB)"
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "Size of Headers", $"0x{optionalHeader.SizeOfHeaders:X} ({optionalHeader.SizeOfHeaders / 1024:F1} kB)"
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem(["CheckSum", $"0x{optionalHeader.CheckSum:X}"]));
        _optionalInformationListView.Items.Add(new ListViewItem(["Subsystem", $"0x{optionalHeader.Subsystem:X}"]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "DLL Characteristics", $"0x{optionalHeader.DllCharacteristics:X}"
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "Size of Stack Reserve",
            $"0x{optionalHeader.SizeOfStackReserve:X} ({optionalHeader.SizeOfStackReserve / 1024:F1} kB)"
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "Size of Stack Commit",
            $"0x{optionalHeader.SizeOfStackCommit:X} ({optionalHeader.SizeOfStackCommit / 1024:F1} kB)"
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "Size of Heap Reserve",
            $"0x{optionalHeader.SizeOfHeapReserve:X} ({optionalHeader.SizeOfHeapReserve / 1024:F1} kB)"
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "Size of Heap Commit",
            $"0x{optionalHeader.SizeOfHeapCommit:X} ({optionalHeader.SizeOfHeapCommit / 1024:F1} kB)"
        ]));
        _optionalInformationListView.Items.Add(new ListViewItem(["Loader Flags", $"0x{optionalHeader.LoaderFlags:X}"]));
        _optionalInformationListView.Items.Add(new ListViewItem([
            "Number of RVA and Sizes", optionalHeader.NumberOfRvaAndSizes.ToString()
        ]));
    }

    private void PopulateSectionInformationListView(IEnumerable<SectionInformation> sectionInformations)
    {
        foreach (var item in sectionInformations.Select(section => new ListViewItem([
                     section.Name,
                     $"0x{section.VirtualSize:X} ({section.VirtualSize / 1024.0:F1} kB)",
                     $"{section.VirtualAddress:X}",
                     $"0x{section.SizeOfRawData:X} ({section.SizeOfRawData / 1024.0:F1} kB)",
                     $"0x{section.PointerToRawData:X}",
                     $"0x{section.PointerToRelocations:X}",
                     $"0x{section.PointerToLinenumbers:X}",
                     section.NumberOfRelocations.ToString(),
                     section.NumberOfLinenumbers.ToString(),
                     section.Characteristics,
                     section.Hash,
                     $"{section.Entropy:F6} S"
                 ])))
            _sectionInformationListView.Items.Add(item);
    }

    private void PopulateDirectoryInformationListView(IEnumerable<DirectoryInformation> directoryInformations)
    {
        foreach (var item in directoryInformations.Select(directory => new ListViewItem([
                     directory.Name,
                     $"0x{directory.Size:X} ({directory.Size / 1024.0:F1} kB)",
                     $"0x{directory.VirtualAddress:X} - 0x{directory.VirtualAddress + directory.Size:X}",
                     directory.Section,
                     $"{directory.Entropy:F6} S",
                     directory.Hash
                 ])))
            _directoryInformationListView.Items.Add(item);
    }

    private void PopulateImportInformationListView(IEnumerable<ImportInformation> importInformations)
    {
        foreach (var item in importInformations.Select(import => new ListViewItem([
                     import.Name,
                     import.UndecoratedName,
                     import.DllName,
                     $"0x{import.Rva:X}",
                     import.Hint.ToString()
                 ])))
            _importInformationListView.Items.Add(item);
    }

    private void PopulateExportInformationListView(IEnumerable<ExportInformation> exportInformations)
    {
        foreach (var item in exportInformations.Select(export => new ListViewItem([
                     export.Name,
                     export.DllName,
                     export.Ordinal.ToString(),
                     $"0x{export.Rva:X}",
                     $"0x{export.Address:X}"
                 ])))
            _exportInformationListView.Items.Add(item);
    }

    private void PopulateLoadConfigInformationListView(LoadConfigInformation loadConfigInformation)
    {
        _loadConfigInformationListView.Items.Add(new ListViewItem(["Size", loadConfigInformation.Size.ToString()]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Time Date Stamp", $"{loadConfigInformation.TimeDateStamp}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Major Version", loadConfigInformation.MajorVersion.ToString()
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Minor Version", loadConfigInformation.MinorVersion.ToString()
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Global Flags Clear", $"0x{loadConfigInformation.GlobalFlagsClear:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Global Flags Set", $"0x{loadConfigInformation.GlobalFlagsSet:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Critical Section Timeout", loadConfigInformation.CriticalSectionDefaultTimeout.ToString()
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "DeCommit Free Block", loadConfigInformation.DeCommitFreeBlockThreshold.ToString()
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "DeCommit Total Free", loadConfigInformation.DeCommitTotalFreeThreshold.ToString()
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Lock Prefix Table", $"0x{loadConfigInformation.LockPrefixTable:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Max Allocation Size", loadConfigInformation.MaximumAllocationSize.ToString()
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Virtual Memory Threshold", loadConfigInformation.VirtualMemoryThreshold.ToString()
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Process Affinity Mask", loadConfigInformation.ProcessAffinityMask.ToString()
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Process Heap Flags", $"0x{loadConfigInformation.ProcessHeapFlags:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "CSD Version", loadConfigInformation.CsdVersion.ToString()
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Dependent Load Flags", $"0x{loadConfigInformation.DependentLoadFlags:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Edit List", $"0x{loadConfigInformation.EditList:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Security Cookie", $"0x{loadConfigInformation.SecurityCookie:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "SE Handler Table", $"0x{loadConfigInformation.SeHandlerTable:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "SE Handler Count", loadConfigInformation.SeHandlerCount.ToString()
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Guard CF Check Func Ptr", $"0x{loadConfigInformation.GuardCfCheckFunctionPointer:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Guard CF Dispatch Ptr", $"0x{loadConfigInformation.GuardCfDispatchFunctionPointer:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Guard CF Func Table", $"0x{loadConfigInformation.GuardCfFunctionTable:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Guard CF Func Count", loadConfigInformation.GuardCfFunctionCount.ToString()
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Guard Flags", loadConfigInformation.GuardFlags
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Guard Address Taken IAT", $"0x{loadConfigInformation.GuardAddressTakenIatEntryTable:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Guard Address Taken IAT Count", loadConfigInformation.GuardAddressTakenIatEntryCount.ToString()
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Guard Long Jump Table", $"0x{loadConfigInformation.GuardLongJumpTargetTable:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Guard Long Jump Count", loadConfigInformation.GuardLongJumpTargetCount.ToString()
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Dynamic Value Reloc Table", $"0x{loadConfigInformation.DynamicValueRelocTable:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Hybrid Metadata Ptr", $"0x{loadConfigInformation.HybridMetadataPointer:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Guard RF Failure Routine", $"0x{loadConfigInformation.GuardRfFailureRoutine:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Guard RF Failure Func Ptr", $"0x{loadConfigInformation.GuardRfFailureRoutineFunctionPointer:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Dynamic Value Reloc Offset", loadConfigInformation.DynamicValueRelocTableOffset.ToString()
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Dynamic Value Reloc Section", loadConfigInformation.DynamicValueRelocTableSection.ToString()
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Guard RF Verify Stack Func Ptr", $"0x{loadConfigInformation.GuardRfVerifyStackPointerFunctionPointer:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Hot Patch Table Offset", loadConfigInformation.HotPatchTableOffset.ToString()
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Enclave Config Ptr", $"0x{loadConfigInformation.EnclaveConfigurationPointer:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Volatile Metadata Ptr", $"0x{loadConfigInformation.VolatileMetadataPointer:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Guard EH Continuation Table", $"0x{loadConfigInformation.GuardEhContinuationTable:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Guard EH Continuation Table Entry Count",
            loadConfigInformation.GuardEhContinuationTableEntryCount.ToString()
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "XFG Check Function Pointer", $"0x{loadConfigInformation.XfgCheckFunctionPointer:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "XFG Dispatch Function Pointer", $"0x{loadConfigInformation.XfgDispatchFunctionPointer:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "XFG Table Dispatch Function Pointer", $"0x{loadConfigInformation.XfgTableDispatchFunctionPointer:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Cast Guard Failure Mode", $"0x{loadConfigInformation.CastGuardFailureMode:X}"
        ]));
        _loadConfigInformationListView.Items.Add(new ListViewItem([
            "Guard Memcpy Function Pointer", $"0x{loadConfigInformation.GuardMemcpyFunctionPointer:X}"
        ]));
    }

    private static string PromptUserToSelectPeFile()
    {
        using OpenFileDialog openFileDialog = new();
        openFileDialog.Filter = @"Executable Files|*.exe;*.dll|All Files|*.*";
        openFileDialog.Title = @"Select a PE File";

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            var selectedFilePath = openFileDialog.FileName;

            if (IsPeFile(selectedFilePath)) return selectedFilePath;
            MessageBox.Show(@"The selected file is not a valid PE file.", @"Invalid File", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            PromptUserToSelectPeFile();
        }

        Application.Exit();
        return string.Empty;
    }
}