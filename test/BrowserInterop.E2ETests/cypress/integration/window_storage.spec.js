/// <reference types="Cypress" />

context('window.storage', () => {
    before(() => {
        cy.visit('/storage');
    });

    it('localStorage length', () => {
        cy.window()
            .then(w => {
                w.localStorage.clear();
                w.localStorage.setItem("aaa", "zzz");
                cy.get("#window-localStorage-length-get").click().then(() => {
                    cy.get('#window-localStorage-length').should('have.text', w.localStorage.length.toString());
                });
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

    it('localStorage getItem', () => {
        cy.window()
            .then(w => {
                w.localStorage.clear();
                w.localStorage.setItem("test", JSON.stringify({ Id: 9999 }));
                cy.get("#window-localStorage-getItem").click().then(() => {
                    cy.get('#window-localStorage-getItem-test').should('have.text', '9999');
                });
            });
    });
    it('localStorage setItem', () => {
        cy.window()
            .then(w => {
                w.localStorage.clear();
                cy.get("#window-localStorage-setItem").click().then(() => {
                    var valueStr = w.localStorage.getItem('test');
                    var value = JSON.parse(valueStr);
                    expect(value).to.have.property('Id').eq(978789);
                });
            });
    });

    it('localStorage removeItem', () => {
        cy.window()
            .then(w => {
                w.localStorage.clear();
                w.localStorage.setItem("test2", "zzz");
                w.localStorage.setItem("test", "zzz");
                cy.get("#window-localStorage-removeItem").click().then(() => {

                    expect(w.localStorage.getItem("test")).to.be.null;
                    expect(w.localStorage.getItem("test2")).to.be.not.null;
                });
            });
    });

    it('localStorage clear', () => {
        cy.window()
            .then(w => {
                w.localStorage.clear();
                w.localStorage.setItem("test2", "zzz");
                w.localStorage.setItem("test", "zzz");
                cy.get("#window-localStorage-clear").click().then(() => {

                    expect(w.localStorage.getItem("test")).to.be.null;
                    expect(w.localStorage.getItem("test2")).to.be.null;
                });
            });
    });

    //for sesion storage we test only length as the class is shared with local
    it('sessionStorage length', () => {
        cy.window()
            .then(w => {
                w.sessionStorage.clear();
                w.sessionStorage.setItem("aaa", "zzz");
                cy.get("#window-sessionStorage-length-get").click().then(() => {
                    cy.get('#window-sessionStorage-length').should('have.text', w.sessionStorage.length.toString());
                });
            });
    });
}
);