/// <reference types="Cypress" />

context('window.console', () => {
    before(() => {
        cy.visit('/console');
    });

    it('Check console methods called', () => {
        cy.window()
            .its('DotNet')
            .should('not.be.undefined')
            .window()
            .then((w) => {
                w.DotNet.jsCallDispatcher.findJSFunction('console.log');
                cy.spy(w.console, "log");
                cy.get("#btn-console-do-test").click()
                    .then(() => {
                        expect(w.console.log).be.called.calledTwice;
                    });


            });
    });
}
);