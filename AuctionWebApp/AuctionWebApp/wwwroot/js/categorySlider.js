window.categorySliderFunctions = {
    scrollElementHorizontal: function (element, amount) {
        if (element) {
            element.scrollBy({ left: amount, behavior: 'smooth' });
        }
    },
    getElementScrollProperties: function (element) {
        if (element) {
            return {
                scrollLeft: element.scrollLeft,
                scrollWidth: element.scrollWidth,
                clientWidth: element.clientWidth
            };
        }
        return null; 
    }
};