/// <reference types="Cypress" />

context('window.console', () => {
    before(() => {
        cy.visit('/console');
    });

    it('Check console methods called', () => {
        cy.window()
            .then((w) => {
                cy.spyFix(w.console, "log", w);
                cy.spyFix(w.console, "assert", w);
                cy.spyFix(w.console, "count", w);
                cy.get("#btn-console-do-test").click()
                    .then(() => {
                        expect(w.console.log).be.calledThrice;
                        expect(w.console.assert).be.callCount(4);
                        expect(w.console.count).be.callCount(5);
                    });
            });
    });
}
);