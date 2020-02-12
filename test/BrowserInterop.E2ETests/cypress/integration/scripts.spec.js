

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
    it('getSerializableObject return only first layer when deep is false', () => {
        cy.window()
            .its('browserInterop')
            .then(b => {
                var obj = { id: 1, inner: { id: 2 } };
                var res = b.getSerializableObject(obj, [], false);
                expect(res).to.have.property('id');
                expect(res).to.not.have.property('inner');
            });
    });

    it('getSerializableObject return all layer when deep is true', () => {
        cy.window()
            .its('browserInterop')
            .then(b => {
                var obj = { id: 1, inner: { id: 2 } };
                var res = b.getSerializableObject(obj, [], true);
                expect(res).to.have.property('id');
                expect(res).to.have.property('inner');
            });
    });


    it('getSerializableObject return all layer when deep is not given', () => {
        cy.window()
            .its('browserInterop')
            .then(b => {
                var obj = { id: 1, inner: { id: 2 } };
                var res = b.getSerializableObject(obj, []);
                expect(res).to.have.property('id');
                expect(res).to.have.property('inner');
            });
    });

    it('getInstancePropertySerializable returns swalow property copy if deep is false', () => {

        cy.window()
            .its('browserInterop')
            .then(b => {
                var obj = { inner: { id: 2, deeper: { id: 3 } } };
                var res = b.getInstancePropertySerializable(obj, 'inner', false);
                expect(res).to.have.property('id');
                expect(res).to.not.have.property('deeper');
            });
    });
    it('getInstancePropertySerializable returns deep property copy if deep is true', () => {

        cy.window()
            .its('browserInterop')
            .then(b => {
                var obj = { inner: { id: 2, deeper: { id: 3 } } };
                var res = b.getInstancePropertySerializable(obj, 'inner', true);
                expect(res).to.have.property('id');
                expect(res).to.have.property('deeper');
            });
    });
    it('getSerializableObject serialize 0', () => {
        cy.window()
            .its('browserInterop')
            .then(b => {
                var obj = { id: 0 };
                var res = b.getInstancePropertySerializable(obj, 'id', false);
                expect(res).to.be.eq(0);
            });
    });
    it('callInstanceMethod apply to sub property if method is in child', () => {
        cy.window()
            .its('browserInterop')
            .then(b => {
                var obj = new (function () {
                    this.id = 1;
                    this.child = new (function () {
                        this.id = 2;
                        this.getId = function () {
                            return this.id;
                        };
                    })();
                })();
                var res = b.callInstanceMethod(obj, 'child.getId');
                expect(res).to.be.eq(2);
            });
    });
});
