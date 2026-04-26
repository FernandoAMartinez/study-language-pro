// swipe-interop.js — touch, mouse-drag, and keyboard swipe detection for flashcards
window.swipeInterop = (function () {
    const _store = {}; // keyed by elementId
    const THRESHOLD = 60; // px horizontal delta to count as a swipe

    function attachSwipe(dotNetRef, elementId) {
        detachSwipe(elementId); // remove any previous listeners first

        const el = document.getElementById(elementId);
        if (!el) return;

        let startX = 0;
        let isDragging = false;

        // ── Touch ─────────────────────────────────────────────────────────────
        const onTouchStart = (e) => {
            startX = e.touches[0].clientX;
        };
        const onTouchEnd = (e) => {
            const delta = e.changedTouches[0].clientX - startX;
            if (Math.abs(delta) >= THRESHOLD) {
                dotNetRef.invokeMethodAsync('OnSwipe', delta > 0 ? 'right' : 'left');
            }
        };

        // ── Mouse drag ────────────────────────────────────────────────────────
        const onMouseDown = (e) => {
            startX = e.clientX;
            isDragging = true;
        };
        const onMouseUp = (e) => {
            if (!isDragging) return;
            isDragging = false;
            const delta = e.clientX - startX;
            if (Math.abs(delta) >= THRESHOLD) {
                dotNetRef.invokeMethodAsync('OnSwipe', delta > 0 ? 'right' : 'left');
            }
        };
        const onMouseLeave = () => { isDragging = false; };

        // ── Keyboard (arrow keys) ─────────────────────────────────────────────
        const onKeyDown = (e) => {
            if (e.key === 'ArrowLeft') {
                e.preventDefault();
                dotNetRef.invokeMethodAsync('OnSwipe', 'left');
            } else if (e.key === 'ArrowRight') {
                e.preventDefault();
                dotNetRef.invokeMethodAsync('OnSwipe', 'right');
            }
        };

        el.addEventListener('touchstart', onTouchStart, { passive: true });
        el.addEventListener('touchend', onTouchEnd);
        el.addEventListener('mousedown', onMouseDown);
        el.addEventListener('mouseup', onMouseUp);
        el.addEventListener('mouseleave', onMouseLeave);
        document.addEventListener('keydown', onKeyDown);

        _store[elementId] = { el, onTouchStart, onTouchEnd, onMouseDown, onMouseUp, onMouseLeave, onKeyDown };
    }

    function detachSwipe(elementId) {
        const s = _store[elementId];
        if (!s) return;

        s.el.removeEventListener('touchstart', s.onTouchStart);
        s.el.removeEventListener('touchend', s.onTouchEnd);
        s.el.removeEventListener('mousedown', s.onMouseDown);
        s.el.removeEventListener('mouseup', s.onMouseUp);
        s.el.removeEventListener('mouseleave', s.onMouseLeave);
        document.removeEventListener('keydown', s.onKeyDown);

        delete _store[elementId];
    }

    return { attachSwipe, detachSwipe };
})();
