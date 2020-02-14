/// <reference types="Cypress" />

context('window.storage', () => {
    before(() => {
        cy.visit('/screen');
    });

    it('screen properties', function () {
        cy.window()
            .then(w => {
                cy.get("#window-screen-AvailWidth").should('have.text', w.screen.availWidth.toString());
                cy.get("#window-screen-AvailHeight").should('have.text', w.screen.availHeight.toString());
                cy.get("#window-screen-ColorDepth").should('have.text', w.screen.colorDepth.toString());
                cy.get("#window-screen-Height").should('have.text', w.screen.height.toString());
                cy.get("#window-screen-Orientation-Type").should('have.text', w.screen.orientation.type);
                cy.get("#window-screen-Orientation-TypeEnum").should('not.be.empty');
                cy.get("#window-screen-Orientation-Angle").should('have.text', w.screen.orientation.angle.toString());
                cy.get("#window-screen-PixelDepth").should('have.text', w.screen.pixelDepth.toString());
                cy.get("#window-screen-Width").should('have.text', w.screen.width.toString());
            });
    });
});