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
    });
    it('window performance clearMeasures', () => {
        cy.window().then((w) => {
            cy.spyFix(w.performance, 'clearMeasures', w);
            cy.get('#btn-window-performance-clearMeasures')
                .click()
                .then(() => {
                    expect(w.performance.clearMeasures).to.be.calledOnce;
                    expect(w.performance.clearMeasures).to.be.calledWith('test2');
                });
        });
    });

    it('window performance clearResourceTimings', () => {
        cy.window().then((w) => {
            cy.spyFix(w.performance, 'clearResourceTimings', w);
            cy.get('#btn-window-performance-clearResourceTimings')
                .click()
                .then(() => {
                    expect(w.performance.clearResourceTimings).to.be.calledOnce;
                    expect(w.performance.clearResourceTimings).to.be.calledWith();
                });
        });
    })


    it('window performance getEntries', () => {
        cy.window().then((w) => {
            var entries = w.performance.getEntries();
            cy.spyFix(w.performance, 'getEntries', w);
            cy.get('#btn-window-performance-getEntries')
                .click()
                .then(() => {
                    expect(w.performance.getEntries).to.be.calledOnce;
                    expect(w.performance.getEntries).to.be.calledWith();
                    //we check only the item count, if there is some bug we'll had more check
                    cy.get("li[performance-type]").should('have.length', entries.length);

                });
        });
    })

    it('window performance getEntriesByName', () => {
        cy.window().then((w) => {
            var entries = w.performance.getEntriesByName("first-paint");
            cy.spyFix(w.performance, 'getEntriesByName', w);
            cy.get('#btn-window-performance-getEntriesByName')
                .click()
                .then(() => {
                    expect(w.performance.getEntriesByName).to.be.calledOnce;
                    expect(w.performance.getEntriesByName).to.be.calledWith("first-paint");
                    //we check only the item count, if there is some bug we'll had more check
                    cy.get("li[performance-type]").should('have.length', entries.length);

                });
        });
    });


    it('window performance getEntriesByName with Type', () => {
        cy.window().then((w) => {
            var entries = w.performance.getEntriesByName("first-paint", "paint");
            cy.spyFix(w.performance, 'getEntriesByName', w);
            cy.get('#btn-window-performance-getEntriesByNameWithType')
                .click()
                .then(() => {
                    expect(w.performance.getEntriesByName).to.be.calledOnce;
                    expect(w.performance.getEntriesByName).to.be.calledWith("first-paint", "paint");
                    //we check only the item count, if there is some bug we'll had more check
                    cy.get("li[performance-type]").should('have.length', entries.length);

                });
        });
    });

    it('window performance getEntriesByType', () => {
        cy.window().then((w) => {
            var entries = w.performance.getEntriesByType("paint");
            cy.spyFix(w.performance, 'getEntriesByType', w);
            cy.get('#btn-window-performance-getEntriesByType')
                .click()
                .then(() => {
                    expect(w.performance.getEntriesByType).to.be.calledOnce;
                    expect(w.performance.getEntriesByType).to.be.calledWith("paint");
                    //we check only the item count, if there is some bug we'll had more check
                    cy.get("li[performance-type]").should('have.length', entries.length);

                });
        });
    });
}
);