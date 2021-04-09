<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Platform Speedrunner Leaderboards | @yield('title')</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- CSRF Token -->
    <meta name="csrf-token" content="{{ csrf_token() }}">
    <link rel="shortcut icon" type="image/x-icon" href="{{ asset('frontend/img/favicon.png') }}">
    @include('inc.head')
</head>
<body class="hold-transition sidebar-mini layout-fixed">
    <div id="preloader" class="preloader">
        <div class="Loading-container">
            <div class="circle"></div>
            <div class="circle"></div>
        </div>
    </div>
    <div class="wrapper">
{{--        @include('inc.slidebar')--}}

        <div class="content-wrapper">
            @yield('content')
        </div>

        <footer class="main-footer">
            <strong>Copyright &copy; 2020</strong>
            All rights reserved.
            <div class="float-right d-none d-sm-inline-block">
                <b>Version</b> 1.0
            </div>
        </footer>

        <aside class="control-sidebar control-sidebar-dark">

        </aside>
    </div>

    @include('inc.javascript')
    @yield('pagejs')
    <script>
        $(window).on('load', function() {
            setTimeout(function () {
                $("#preloader").delay(600).fadeOut(600).addClass('d-none');
            }, 700);
        });
    </script>
</body>
</html>
