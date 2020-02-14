/// <reference types="Cypress" />

context('window.performance', () => {
    before(() => {
        cy.visit('/performance');
    });
    it('window performance timeOrigin', () => {
        cy.window().then((w) => {
            cy.get('#window-performance-timeorigin').should('have.text', w.performance.timeOrigin.toFixed(2).toString());
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

    it('window performance mark', () => {
        cy.window().then((w) => {
            cy.spyFix(w.performance, 'mark', w);
            cy.get('#btn-window-performance-mark')
                .click()
                .then(() => {
                    expect(w.performance.mark).to.be.calledOnce;
                    expect(w.performance.mark).to.be.calledWith("testmark");
                });
        });
    });


    it('window performance measure', () => {
        cy.window().then((w) => {
            cy.spyFix(w.performance, 'measure', w);
            cy.get('#btn-window-performance-measure')
                .click()
                .then(() => {
                    expect(w.performance.measure).to.be.calledOnce;
                    expect(w.performance.measure).to.be.calledWith("testmeasure");
                });
        });
    });



    it('window performance now', () => {
        cy.window().then((w) => {
            cy.stubFix(w.performance, 'now', w, function () { return 1.123456789 });
            cy.get('#btn-window-performance-now')
                .click()
                .then(() => {
                    //c# timespan is precise to Ticks
                    cy.get('#window-performance-now').should('have.text', '1.123456789');
                });
        });
    });

    it('window performance setResourceTimingBufferSize', () => {
        cy.window().then((w) => {
            cy.spyFix(w.performance, 'setResourceTimingBufferSize', w);
            cy.get('#btn-window-performance-setResourceTimingBufferSize')
                .click()
                .then(() => {
                    expect(w.performance.setResourceTimingBufferSize).to.be.calledOnce;
                    expect(w.performance.setResourceTimingBufferSize).to.be.calledWith(123456789);
                });
        });
    });
}
);