/// <reference types="Cypress" />

context('window.history', () => {
    before(() => {
        cy.visit('/history');
    });
    it('window history length', () => {
        cy.window()
            .then(w => {
                cy.get("#window-history-length").should('have.text', w.history.length.toString());
            });

    });
    it('window history scrollRestoration', () => {
        cy.window()
            .then(w => {
                cy.get("#window-history-scrollRestaurationEnum").should('have.text', w.history.scrollRestoration);
                cy.get('#window-history-scrollRestoration-set').click().then(
                    () => {
                        cy.get("#window-history-scrollRestaurationEnum").should('have.text', 'manual');
                    }
                )
            });

    });

    it('window history state', () => {
        cy.window()
            .then(w => {
                cy.get('#window-history-state-get')
                    .click()
                    .then(() => {
                        cy.get('#window-history-state').should('not.exist');
                        cy.get('#window-history-state-pushState').click()
                            .then(() => {
                                cy.get('#window-history-state').should('have.text', '123456');

                            });
                    })
            });

    });
});
