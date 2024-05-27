$(function () {
    var l = abp.localization.getResource('TheGames');
    var createModal = new abp.ModalManager(abp.appPath + 'Publishers/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Publishers/EditModal');

    var dataTable = $('#PublishersTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(theGames.publishers.publisher.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('TheGames.Publishers.Edit'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('TheGames.Publishers.Delete'), //CHECK for the PERMISSION
                                    confirmMessage: function (data) {
                                        return l('PublisherDeletionConfirmationMessage');
                                    },
                                    action: function (data) {
                                        theGames.publishers.publisher
                                            .delete(data.record.id)
                                            .then(function() {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Name'),
                    data: "name",
                },
                {
                    title: l('Country'),
                    data: "country",
                },
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewPublisherButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
