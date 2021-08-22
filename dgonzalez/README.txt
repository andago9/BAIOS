Output Files

    Two output files are generated
        FrameView-<Application exe name>-YYYY-MM-DDTHHMMSS_Log.csv - contains per frame data
        FrameView-<Application exe name>_Summary.csv - contains aggregate statistics
    

    Columns in FrameView-<Application exe name>-YYYY-MM-DDTHHMMSS_Log.csv
        Application                 The name of the process that called Present (if known)
        GPU
        CPU
        Resolution
        Runtime
        AllowsTearing               Whether tearing is possible (1) or not (0)       	
        ProcessID                   The process ID of the process that called Present	
        SwapChainAddress            The address of the swap chain that was presented into	
        Runtime                     The runtime used to present (e.g., D3D9 or DXGI)	
        SyncInterval                Sync interval used in the Present call	
        PresentFlags                Flags used in the Present call	
        PresentMode                 The time of the Present call, measured from when FrameView/PresentMon recording started in seconds	
        Dropped                     Whether the frame was dropped (1) or displayed (0); if dropped, MsUntilDisplayed will be 0	
        TimeInSeconds               The time of the Present call, measured from when FrameView/PresentMon recording started in seconds	
        MsBetweenPresents           The time between this Present call and the previous one, in milliseconds	
        MsBetweenDisplayChange      The time between when the previous frame was displayed and this frame was, in milliseconds	
        MsInPresentAPI              The time spent inside the Present call, in milliseconds	
        MsRenderPresentLatency      The time between the Present call (TimeInSeconds) and when the GPU work completed, in milliseconds	
        MsUntilDisplayed            The time between the Present call (TimeInSeconds) and when the frame was displayed, in milliseconds	
        Render Queue Depth
        GPU0Clk(MHz)
        GPU0Util(%)
        GPU0Temp(C)
        GPU1Clk(MHz)
        GPU1Util(%)
        GPU1Temp(C)
        PCAT Power Total(W)
        Perf/W Total(F/J) (PCAT)     Performance per Watt considering MsBetweenPresents for performance and board power reported by PCAT
        Perf/W Total(F/J) (API)      Performance per Watt considering MsBetweenPresents for performance and board power reported by API
        Perf/W GPUOnly(F/J) (API)    Performance per Watt considering MsBetweenPresents for performance and GPU/Chip/ASIC power reported by API
        Perf/W Total-USBC(F/J) (API) Performance per Watt considering MsBetweenPresents for performance and board power excluding USBC reported by API
        GPUOnlyPwr(W) (API)          GPU/Chip/ASIC power
        NV-Total-USBCPwr(W) (API)    Board power excluding USBC
        NV Pwr(W) (API)              Board power
        AMDPwr(W) (API)
        CPUClk(MHz)
        CPUUtil(%)
        CPU Package Temp(C)
        CPU Package Power(W)
        CPU TDP (W)
        CPUCoreUtil%[0..63]

    Columns in FrameView-<Application exe name>_Summary.csv
        Timestamp
        Application                 The name of the process that called Present (if known)
        Log Name
        GPU0
        GPU1
        CPU
        Resolution
        Runtime
        Avg FPS
        Min FPS
        Max FPS
        90th %
        95th %
        99th %
        Time (ms)
        RenderPresentLatency (ms)
        GPU0Clk(MHz)
        GPU0 Util%
        GPU0Temp(C)
        GPU1Clk(MHz)
        GPU1Util(%)
        GPU1Temp(C)
        Perf/Watt (F/J) (PCAT)
        PCAT Power (Watts)
        CPUClk(MHz)
        CPU Util %
        CPU Temp (C)
        OS
        GPU Base Driver
        GPU Driver Package
        System RAM
        Motherboard

Blacklisting processes for logging and overlay
    To blacklist a process for overlay add an entry to blacklist.overlay.txt file
    To blacklist a process for logging e.g. unwanted logs for browser running in the background add an entry to blacklist.txt file

Settings 
    Settings.ini has knobs to toggle features ON/OFF. Only toggle settings listed below.
    FPSOverlayDispMode              Bitmask (accepts both hex or decimal)
                                    0x800 - Enable per CPU core/thread utilization
