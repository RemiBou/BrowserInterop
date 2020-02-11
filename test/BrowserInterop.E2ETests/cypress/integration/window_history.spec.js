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

    it('window history back', () => {
        cy.window()
            .then(w => {
                cy.stubFix(w.history, 'back', w, () => { });
                cy.get("#window-history-back")
                    .click()
                    .then(() => expect(w.history.back).to.be.calledOnce);
            });

    });
    it('window history forward', () => {
        cy.window()
            .then(w => {
                cy.stubFix(w.history, 'forward', w, () => { });
                cy.get("#window-history-forward")
                    .click()
                    .then(() => expect(w.history.forward).to.be.calledOnce);
            });

    });

    it('window history go', () => {
        cy.window()
            .then(w => {
                cy.stubFix(w.history, 'go', w, () => { });
                cy.get("#window-history-go")
                    .click()
                    .then(() => expect(w.history.go).to.be.calledWith(-2));
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
                                cy.get('#window-history-state-get')
                                    .click()
                                    .then(() => {
                                        cy.get('#window-history-state').should('have.text', '123456');
                                    });

                            });
                    })
            });

    });

    it('window history replaceState', () => {
        cy.window()
            .then(w => {
                cy.get('#window-history-state-pushState').click()
                    .then(() => {
                        cy.get('#window-history-state-replaceState')
                            .click()
                            .then(() => {

                                cy.get('#window-history-state-get')
                                    .click()
                                    .then(() => {
                                        cy.get('#window-history-state').should('have.text', '1234567');
                                    });
                            });

                    });
            });

    });
});
