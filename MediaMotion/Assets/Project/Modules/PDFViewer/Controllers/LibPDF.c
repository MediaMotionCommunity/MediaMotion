// Using MuPDF lib
#include <mupdf/fitz.h>

/**
    How to compile:

    Mac OS:
        compiled MuPDF in $CCDIR
        dll path in $OUT
        gcc -dynamiclib -o $(OUT)/libpdf.bundle ./LibPDF.c $CCDIR/build/debug/*.a -I$CCDIR/include -lm

        Example:
            gcc -dynamiclib -o ../../../../lib/libpdf.bundle ./LibPDF.c \
            /Users/vincentbrunet/Downloads/mupdf-1.7-source/build/debug/*.a \
            -I/Users/vincentbrunet/Downloads/mupdf-1.7-source/include/ -lm
 */

/**
 * Session allocation
 */
fz_context *libpdf_load_session()
{
    fz_context *ctx = fz_new_context(NULL, NULL, FZ_STORE_UNLIMITED);
    fz_register_document_handlers(ctx);
    return ctx;
}
void libpdf_free_session(fz_context *ctx)
{
    fz_drop_context(ctx);
}

/**
 * Document allocation
 */
fz_document *libpdf_load_document(fz_context *ctx, const char *filename)
{
    return fz_open_document(ctx, filename);
}
void libpdf_free_document(fz_context *ctx, fz_document *doc)
{
    fz_drop_document(ctx, doc);
}

/**
 * Single page allocation
 */
typedef struct fz_frame fz_frame;
struct fz_frame {
    // View matrix
    fz_matrix transform;
    fz_rect bounds;
    fz_irect bbox;
    // Structure
    fz_page *page;
    fz_pixmap *pix;
    fz_device *dev;
};
fz_frame *libpdf_load_page(fz_context *ctx, fz_document *doc, int pagenum)
{
    // Allocate page
    fz_frame *frame = malloc(sizeof(fz_frame));
    frame->page = fz_load_page(ctx, doc, pagenum);
    // Get page bounding box and render transform
    fz_rotate(&frame->transform, 0);
    fz_pre_scale(&frame->transform, 1.0, 1.0);
    fz_bound_page(ctx, frame->page, &frame->bounds);
    fz_transform_rect(&frame->bounds, &frame->transform);
    fz_round_rect(&frame->bbox, &frame->bounds);
    // Allocate pixmap
    frame->pix = fz_new_pixmap_with_bbox(ctx, fz_device_rgb(ctx), &frame->bbox);
    fz_clear_pixmap_with_value(ctx, frame->pix, 0xff);
    // Allocate render device
    frame->dev = fz_new_draw_device(ctx, frame->pix);

    return frame;
}
void libpdf_free_page(fz_context *ctx, fz_frame *frame)
{
    fz_drop_device(ctx, frame->dev);
    fz_drop_pixmap(ctx, frame->pix);
    fz_drop_page(ctx, frame->page);
    free(frame);
}

/**
 * Usefull functions
 */
int libpdf_count_pages(fz_context *ctx, fz_document *doc)
{
    return fz_count_pages(ctx, doc);
}
void libpdf_render_page(fz_context *ctx, fz_frame *frame)
{
    fz_run_page(ctx, frame->page, frame->dev, &frame->transform, NULL);
}

int libpdf_xsize_page(fz_context *ctx, fz_frame *frame)
{
    return fz_pixmap_width(ctx, frame->pix);
}
int libpdf_ysize_page(fz_context *ctx, fz_frame *frame)
{
    return fz_pixmap_height(ctx, frame->pix);
}
void *libpdf_pixels_page(fz_context *ctx, fz_frame *frame)
{
    return fz_pixmap_samples(ctx, frame->pix);
}
