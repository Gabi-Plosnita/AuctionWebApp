window.stripePayment = {
    initialize: async function (publishableKey) {
        if (!window.Stripe) {
            console.error("Stripe.js not loaded");
            return;
        }

        const stripe = Stripe(publishableKey);
        const elements = stripe.elements();

        const card = elements.create("card");
        card.mount("#card-element");

        window.stripeInstance = { stripe, card };
    },

    createPaymentMethod: async function (cardholderName) {
        const { stripe, card } = window.stripeInstance;

        const result = await stripe.createPaymentMethod({
            type: "card",
            card: card,
            billing_details: { name: cardholderName }
        });

        if (result.error) {
            console.error(result.error.message);
            return { error: result.error.message };
        }

        return { paymentMethodId: result.paymentMethod.id };
    },

    clearCardElement: function () {
        const { card } = window.stripeInstance || {};
        if (card && typeof card.clear === "function") {
            card.clear();
        }
    }
};

window.getInputValue = function (id) {
    return document.getElementById(id)?.value || "";
};
