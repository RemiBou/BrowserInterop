

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


    it('getInstancePropertySerializable return null if property null', () => {
        cy.window()
            .its('browserInterop')
            .then(b => {
                var obj = { field: null };
                expect(b.getInstancePropertySerializable(obj, "field")).to.eq(null);
            });
    });

    it('callInstanceMethod call instance method with parameters', () => {
        cy.window()
            .its('browserInterop')
            .then(b => {
                var obj = { method: function (a, b) { } };
                cy.spy(obj, 'method');
                b.callInstanceMethod(obj, "method", "A", "B")
                expect(obj.method).to.be.called.calledWith("A", "B");
            });
    });
});
