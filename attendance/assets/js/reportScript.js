var baseUrl = window.location.origin;
$(document).ready(function () {
    if ($('.branch').length) {
        $('.branch').on('change', function () {
            var id = $(this).val();
            $('.branchId').val(id);
            if ($('.department').length) {
                $('.department option').remove();
                $('.departmentId').val('');
                $.ajax({
                    method: 'post',
                    url: baseUrl + '/function.aspx/getDeptByBranchId',
                    data: '{ "id" : ' + id + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    async: false,
                    success: function (result) {
                        result = result.d;
                        if (result.length > 0) {
                            $('.department').append('<option value="">Select Department</option>');
                            result.forEach(function (e, i) {
                                $('.department').append('<option value="' + e.DEPT_ID + '">' + e.DEPT_NAME + '</option>');
                            });
                        } else {
                            $('.department').val('');
                            $('.departmentId').val('');
                        }
                    }
                })
            }
            if ($('.employee').length) {
                $('.employee option').remove();
                $('.employeeId').val('');
                $.ajax({
                    method: 'post',
                    url: baseUrl + '/function.aspx/getEmpByBranchId',
                    data: '{ "id" : ' + id + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    async: false,
                    success: function (result) {
                        result = result.d;
                        if (result.length > 0) {
                            $('.employee').append('<option value="">Select Employee</option>');
                            result.forEach(function (e, i) {
                                $('.employee').append('<option value="' + e.EMP_ID + '">' + e.emp_Fullname + '</option>');
                            });
                        } else {
                            $('.employee').val('');
                            $('.employeeId').val('');
                        }
                    }
                })
            }
        })
    }

    if ($('.department').length) {
        $('.department').on('change', function () {
            var id = $(this).val();
            $('.departmentId').val(id);
            if ($('.employee').length) {
                $('.employee option').remove();
                $('.employeeId').val('');
                $.ajax({
                    method: 'post',
                    url: baseUrl + '/function.aspx/getEmpByDeptId',
                    data: '{ "id" : ' + id + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    async: false,
                    success: function (result) {
                        result = result.d;
                        if (result.length > 0) {
                            $('.employee').append('<option value="">Select Employee</option>');
                            result.forEach(function (e, i) {
                                $('.employee').append('<option value="' + e.EMP_ID + '">' + e.emp_Fullname + '</option>');
                            });
                        } else {
                            $('.employee').val('');
                            $('.employeeId').val('');
                        }
                    }
                })
            }
            if ($('.branch').length) {
                $.ajax({
                    method: 'post',
                    url: baseUrl + '/function.aspx/getBraByDeptId',
                    data: '{ "id" : ' + id + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    async: false,
                    success: function (result) {
                        result = result.d;
                        if (result.length > 0) {
                            var branchId = result[0]['BRANCH_ID'];
                            if ($('.branch').val() != branchId) {
                                $('.branch').val(branchId).prop('selected', true);
                                $('.branchId').val(branchId);
                            }
                        } else {
                            $('.branch').val('');
                            $('.branchId').val('');
                        }
                    }
                })
            }
        })
    }

    function empAjax(id) {
        $.ajax({
            method: 'post',
            url: baseUrl + '/function.aspx/getDataByEmpId',
            data: '{ "id" : ' + id + '}',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            async: false,
            success: function (result) {
                result = result.d;
                if (result.length > 0) {
                    var branchId = result[0]['BRANCH_ID'];
                    var departmentId = result[0]['DEPT_ID'];
                    if ($('.branch').val() != branchId) {
                        $('.branch').val(branchId).prop('selected', true);
                        $('.branchId').val(branchId);
                    }
                    if ($('.department').val() != departmentId) {
                        getAllDepartment();
                        $('.department').val(departmentId).prop('selected', true);
                        $('.departmentId').val(departmentId);
                    }
                    if ($('.employee').val() != id) {
                        getAllEmployee();
                        $('.employee').val(id).prop('selected', true);
                    }
                } else {
                    $('.branch').val('');
                    $('.branchId').val('');
                    $('.department').val('');
                    $('.departmentId').val('');
                }
            }
        });
    }

    if ($('.employee').length) {
        $('.employee').on('change', function () {
            var id = $(this).val();
            $('.employeeId').val(id);
            empAjax(id);
        })
    }

    var timer;
    $('.employeeId').keyup(function () {
        clearTimeout(timer);
        timer = setTimeout(function () {
            var id = $('.employeeId').val();
            empAjax(id);
        }, 1000);
    })

    $('select').on('change', function () {
        ifSelectClear($(this).attr('id'));
    })

    $('input[type="checkbox"]').on('click', function () {
        if ($(this).is(":checked")) {
            isChecked($(this).attr('id'));
        } else {
            isUnchecked($(this).attr('id'));
        }
    })

    function getAllDepartment() {
        $.ajax({
            method: 'post',
            url: baseUrl + '/function.aspx/getAllDepartment',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            async: false,
            success: function (result) {
                result = result.d;
                if (result.length > 0) {
                    $('.department').append('<option value="">Select Department</option>');
                    result.forEach(function (e, i) {
                        $('.department').append('<option value="' + e.DEPT_ID + '">' + e.DEPT_NAME + '</option>');
                    });
                }
            }
        })
    }

    function getAllEmployee() {
        $.ajax({
            method: 'post',
            url: baseUrl + '/function.aspx/getAllEmployee',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            async: false,
            success: function (result) {
                result = result.d;
                if (result.length > 0) {
                    $('.employee').append('<option value="">Select Employee</option>');
                    result.forEach(function (e, i) {
                        $('.employee').append('<option value="' + e.EMP_ID + '">' + e.emp_Fullname + '</option>');
                    });
                }
            }
        })
    }

    function isChecked(param) {
        if (param == "content_allBranch") {
            if ($('.branch').length) {
                $('.branch').prop('required', false);
                $('.branch').prop("selectedIndex", 0).val();
            }
            if ($('.branchId').length) {
                $('.branchId').val('');
                $('.branchId').prop('required', false);
            }
            if ($('.allDepartment').length) {
                $('.allDepartment').prop('checked', true);
            }
        }
        if (param == "content_allBranch" || param == "content_allDepartment") {
            if ($('.department').length) {
                getAllDepartment();
                $('.department').prop('required', false);
                $('.department').prop("selectedIndex", 0).val();
            }
            if ($('.departmentId').length) {
                $('.departmentId').val('');
                $('.departmentId').prop('required', false);
            }
            if ($('.allEmployee').length) {
                $('.allEmployee').prop('checked', true);
            }
        }
        if (param == "content_allBranch" || param == "content_allDepartment" || param == "content_allEmployee") {
            if ($('.employee').length) {
                getAllEmployee();
                $('.employee').prop('required', false);
                $('.employee').prop("selectedIndex", 0).val();
            }
            if ($('.employeeId').length) {
                $('.employeeId').val('');
                $('.employeeId').prop('required', false);
            }
        }
    }

    function isUnchecked(param) {
        if (param == "content_allBranch") {
            if ($('.branch').length) {
                $('.branch').prop('required', true);
                $('.branch').prop("selectedIndex", 0).val();
            }
            if ($('.branchId').length) {
                $('.branchId').val('');
                $('.branchId').prop('required', true);
            }
            if ($('.allDepartment').length) {
                $('.allDepartment').prop('checked', false);
            }
        }
        if (param == "content_allBranch" || param == "content_allDepartment") {
            if ($('.department').length) {
                getAllDepartment();
                $('.department').prop('required', true);
                $('.department').prop("selectedIndex", 0).val();
            }
            if ($('.departmentId').length) {
                $('.departmentId').val('');
                $('.departmentId').prop('required', true);
            }
            if ($('.allEmployee').length) {
                $('.allEmployee').prop('checked', false);
            }
        }
        if (param == "content_allBranch" || param == "content_allDepartment" || param == "content_allEmployee") {
            if ($('.employee').length) {
                getAllEmployee();
                $('.employee').prop('required', true);
                $('.employee').prop("selectedIndex", 0).val();
            }
            if ($('.employeeId').length) {
                $('.employeeId').val('');
                $('.employeeId').prop('required', true);
            }
        }
    }

    function ifSelectClear(param) {
        if (param == "content_branch" || param == "content_department" || param == "content_employee") {
            if ($('.branch').length) {
                $('.allBranch').prop('checked', false);
                $('.branch').prop('required', true);
                $('.branchId').prop('required', true);
            }
        }
        if (param == "content_department" || param == "content_employee") {
            if ($('.department').length) {
                $('.allDepartment').prop('checked', false);
                $('.department').prop('required', true);
                $('.departmentId').prop('required', true);
            }
        }
        if (param == "content_employee") {
            if ($('.employee').length) {
                $('.allEmployee').prop('checked', false);
                $('.employee').prop('required', true);
                $('.employeeId').prop('required', true);

            }
        }
    }
})