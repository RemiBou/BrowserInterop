/// <reference types="Cypress" />

context('window.navigator', () => {
    before(() => {
        cy.visit('/window');
    });
    it('Check window frames', () => {
        cy.get('#child-iframe')
            .iframe()
            .then((i) => {
                i.contents().find('h1');
                cy.window()
                    .then(w => {
                        console.log(window.origin, w.origin, w.frames[0].origin);
                        cy.spyFix(w.frames[0].console, 'log', w.frames[0]);
                        cy.get('#btn-window-child-frame-console').click().then(
                            () => {
                                expect(w.frames[0].console.log).to.be.calledOnce
                                expect(w.frames[0].console.log).to.be.calledWith("test child");
                            }
                        )
                    });
            });
    });
});
