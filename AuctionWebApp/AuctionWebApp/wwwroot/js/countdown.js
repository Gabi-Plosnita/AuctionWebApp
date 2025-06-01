window.startCountdown = function (endTimeIsoString) {
    const endTime = new Date(endTimeIsoString);
    const countdownEl = document.getElementById("countdown");

    function updateCountdown() {
        if (!countdownEl) return;

        const now = new Date();
        const diff = endTime - now;

        if (diff <= 0) {
            countdownEl.textContent = "Auction ended";
            clearInterval(timer);
            return;
        }

        const days = Math.floor(diff / (1000 * 60 * 60 * 24));
        const hours = Math.floor((diff / (1000 * 60 * 60)) % 24);
        const minutes = Math.floor((diff / 1000 / 60) % 60);
        const seconds = Math.floor((diff / 1000) % 60);

        let parts = [];
        if (days > 0) parts.push(`${days}d`);
        if (hours > 0) parts.push(`${hours}h`);
        if (minutes > 0) parts.push(`${minutes}m`);
        if (seconds > 0 || parts.length === 0) parts.push(`${seconds}s`);

        countdownEl.textContent = parts.join(" ");
    }

    updateCountdown();
    const timer = setInterval(updateCountdown, 1000);
}
