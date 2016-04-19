var ResponsiveUtilities = {
    getSize: function () {
        var w = window.innerWidth;
        
        if(w >= 1200) return 3; // LG
        if(w >= 992) return 2; // MD
        if(w >= 768) return 1; // SM
        
        return 0; // XS
    }
}; 