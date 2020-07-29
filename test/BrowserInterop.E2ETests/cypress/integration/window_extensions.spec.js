/// <reference types="Cypress" />

context('extensions', () => {
        before(() => {
            cy.visit('/extension');
        });

        it('callback result used', () => {
            cy.window()
                .then((w) => {
                    
                    cy.get("#btn-extension-callbacktest").click()
                        .then(() => {
                            expect(w.testObject.lastValue).be.eq(4);
                            cy.get("#btn-extension-callbacktestrefresh")
                                .click()
                                .then(() => cy.get("#extension-callback-lastvalue").should('have.text', '4'))
                        });
                });
        });
    }
);