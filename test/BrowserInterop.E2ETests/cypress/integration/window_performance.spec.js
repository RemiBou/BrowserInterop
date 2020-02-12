/// <reference types="Cypress" />

context('window.performance', () => {
    before(() => {
        cy.visit('/performance');
    });
    it('window performance timeOrigin', () => {
        cy.window().then((w) => {
            cy.get('#window-performance-timeorigin').should('have.text', (Math.floor(w.performance.timeOrigin)).toString());
        });
    })
}
);