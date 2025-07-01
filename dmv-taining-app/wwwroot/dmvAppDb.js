// dmvAppDb.js: IndexedDB logic for DMV App
window.dmvAppDb = {
    db: null,
    openDb: function () {
        return new Promise((resolve, reject) => {
            if (this.db) return resolve(this.db);
            const request = indexedDB.open('DmvAppDb', 1);
            request.onupgradeneeded = function (event) {
                const db = event.target.result;
                if (!db.objectStoreNames.contains('applications')) {
                    db.createObjectStore('applications', { keyPath: 'ApplicationNumber' });
                }
                if (!db.objectStoreNames.contains('meta')) {
                    db.createObjectStore('meta');
                }
            };
            request.onsuccess = function (event) {
                window.dmvAppDb.db = event.target.result;
                resolve(window.dmvAppDb.db);
            };
            request.onerror = function (event) {
                reject(event.target.error);
            };
        });
    },
    getLastAppNumber: async function () {
        const db = await this.openDb();
        return new Promise((resolve) => {
            const tx = db.transaction('meta', 'readonly');
            const store = tx.objectStore('meta');
            const req = store.get('lastAppNumber');
            req.onsuccess = function () {
                resolve(req.result || 0);
            };
            req.onerror = function () {
                resolve(0);
            };
        });
    },
    saveApplication: async function (appJson, appNumber) {
        const db = await this.openDb();
        const app = JSON.parse(appJson);
        app.ApplicationNumber = appNumber;
        return new Promise((resolve, reject) => {
            const tx = db.transaction(['applications', 'meta'], 'readwrite');
            tx.objectStore('applications').put(app);
            tx.objectStore('meta').put(appNumber, 'lastAppNumber');
            tx.oncomplete = function () { resolve(); };
            tx.onerror = function (e) { reject(e); };
        });
    },
    getAllApplications: async function () {
        const db = await this.openDb();
        return new Promise((resolve) => {
            const tx = db.transaction('applications', 'readonly');
            const store = tx.objectStore('applications');
            const req = store.getAll();
            req.onsuccess = function () {
                resolve(JSON.stringify(req.result || []));
            };
            req.onerror = function () {
                resolve(JSON.stringify([]));
            };
        });
    },
    getApplicationByNumber: async function (appNumber) {
        const db = await this.openDb();
        return new Promise((resolve) => {
            const tx = db.transaction('applications', 'readonly');
            const store = tx.objectStore('applications');
            const req = store.get(appNumber);
            req.onsuccess = function () {
                resolve(JSON.stringify(req.result || null));
            };
            req.onerror = function () {
                resolve(null);
            };
        });
    },
    clearApplications: async function () {
        const db = await this.openDb();
        return new Promise((resolve, reject) => {
            const tx = db.transaction(['applications', 'meta'], 'readwrite');
            tx.objectStore('applications').clear();
            tx.objectStore('meta').put(0, 'lastAppNumber');
            tx.oncomplete = function () { resolve(); };
            tx.onerror = function (e) { reject(e); };
        });
    }
};

window.dmvAppModal = {
    show: function (id) {
        var modalEl = document.getElementById(id);
        if (modalEl) {
            var modal = bootstrap.Modal.getOrCreateInstance(modalEl);
            modal.show();
        }
    },
    hide: function (id) {
        var modalEl = document.getElementById(id);
        if (modalEl) {
            var modal = bootstrap.Modal.getOrCreateInstance(modalEl);
            modal.hide();
        }
    }
};
