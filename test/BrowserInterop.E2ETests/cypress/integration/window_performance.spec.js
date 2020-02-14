/// <reference types="Cypress" />

context('window.performance', () => {
    before(() => {
        cy.visit('/performance');
    });
    it('window performance timeOrigin', () => {
        cy.window().then((w) => {
            cy.get('#window-performance-timeorigin').should('have.text', (Math.floor(w.performance.timeOrigin)).toString());
        });
    });


    it('window performance clearMarks', () => {
        cy.window().then((w) => {
            cy.spyFix(w.performance, 'clearMarks', w);
            cy.get('#btn-window-performance-clearMarks')
                .click()
                .then(() => {
                    expect(w.performance.clearMarks).to.be.calledOnce;
                    expect(w.performance.clearMarks).to.be.calledWith('test');
                });
        });
    })
}
);