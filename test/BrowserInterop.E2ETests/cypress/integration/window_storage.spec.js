/// <reference types="Cypress" />

context('window.storage', () => {
    before(() => {
        cy.visit('/storage');
    });

    it('localStorage length', () => {
        cy.window()
            .then(w => {
                cy.get("#window-localStorage-length").should("have.text", w.localStorage.length.toString());
            });
    });

    it('localStorage key', () => {
        cy.window()
            .then(w => {
                w.localStorage.clear();
                w.localStorage.setItem("aaa", "zzz");
                cy.get("#window-localStorage-key-get").click().then(() => {
                    cy.get("#window-localStorage-key").should("have.text", "aaa");
                });
            });
    });

    it('localStorage setItem', () => {
    });

    it('localStorage removeItem', () => {
    });

    it('localStorage clear', () => {
    });

    //for sesion storage we test only length as the class is shared with local
    it('sessionStorage length', () => {
    });
}
);