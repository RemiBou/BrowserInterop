

context('scripts', () => {
    before(() => {
        cy.visit('/navigator');
    });
    it('setInstanceProperty set property value', () => {
        cy.window()
            .its('browserInterop')
            .then(b => {
                var obj = { inner: { id: 1 } };
                b.setInstanceProperty(obj, "inner.id", 2);
                expect(obj.inner.id).to.eq(2);
            });
    });

    it('getInstanceProperty return property', () => {
        cy.window()
            .its('browserInterop')
            .then(b => {
                var obj = { inner: { id: 1 } };
                expect(b.getInstanceProperty(obj, "inner.id")).to.eq(1);
            });
    });
});
