/// <reference types="Cypress" />

context('window.console', () => {
    before(() => {
        cy.visit('/console');
    });

    it('Check console methods called', () => {
        cy.window()
            .then((w) => {
                cy.spy(w.console, "log");
                w.console.log.__proto__ = w.Function;
                cy.get("#btn-console-do-test").click()
                    .then(() => {
                        expect(w.console.log).be.called.calledThrice;
                    });
            });
    });
}
);