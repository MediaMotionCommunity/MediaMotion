// Using MuPDF lib
#include <mupdf/fitz.h>

/**
 * Session allocation
 */
fz_context *libpdf_load_session()
{
    fz_context *ctx;
    ctx = fz_new_context(NULL, NULL, FZ_STORE_UNLIMITED);
    fz_register_document_handlers(ctx);
    return ctx;
}
void libpdf_free_session(fz_context *ctx)
{
    if (ctx != NULL) {
        fz_drop_context(ctx);
    }
}

/**
 * Document allocation
 */
fz_document *libpdf_load_document(fz_context *ctx, const char *filename)
{
    fz_document *doc;
    fz_try(ctx) {
        doc = fz_open_document(ctx, filename);
    }
    fz_catch(ctx) {
        doc = NULL;
    }
    return doc;
}
void libpdf_free_document(fz_context *ctx, fz_document *doc)
{
    if (ctx != NULL && doc != NULL) {
        fz_try(ctx) {
            fz_drop_document(ctx, doc);
        }
        fz_catch(ctx) {
        }
    }
}

/**
 * Single page allocation
 */
typedef struct fz_frame fz_frame;
struct fz_frame {
    // Page ratio
    float xratio;
    float yratio;
    // View matrix
    fz_matrix transform;
    fz_rect bounds;
    fz_irect bbox;
    // Structure
    fz_page *page;
    fz_pixmap *pix;
    fz_device *dev;
    // Error flag
    int error;
};
fz_frame *libpdf_load_page(fz_context *ctx, fz_document *doc, int pagenum, float xdpi, float ydpi)
{
    // Allocate page
    fz_frame *frame = malloc(sizeof(fz_frame));
    fz_try(ctx) {
        // Prepare page
        frame->error = 1;
        frame->page = fz_load_page(ctx, doc, pagenum);
        frame->xratio = xdpi / 72.0;
        frame->yratio = ydpi / 72.0;
        // Get page bounding box and render transform
        frame->error = 2;
        fz_rotate(&frame->transform, 0);
        fz_pre_scale(&frame->transform, frame->xratio, frame->yratio);
        fz_bound_page(ctx, frame->page, &frame->bounds);
        fz_transform_rect(&frame->bounds, &frame->transform);
        fz_round_rect(&frame->bbox, &frame->bounds);
        // Allocate pixmap
        frame->error = 3;
        frame->pix = fz_new_pixmap_with_bbox(ctx, fz_device_rgb(ctx), &frame->bbox);
        // Allocate render device
        frame->error = 4;
        frame->dev = fz_new_draw_device(ctx, frame->pix);
        // Done
        frame->error = 0;
    }
    // Loading error
    fz_catch(ctx) {
        free(frame);
        frame = NULL;
    }
    return frame;
}
void libpdf_free_page(fz_context *ctx, fz_frame *frame)
{
    // Free contents
    if (frame != NULL && ctx != NULL) {
        fz_try(ctx) {
            fz_drop_device(ctx, frame->dev);
            fz_drop_pixmap(ctx, frame->pix);
            fz_drop_page(ctx, frame->page);
        }
        fz_catch(ctx) {
        }
    }
    // Free container
    if (frame != NULL) {
        free(frame);
    }
}

/**
 * Usefull functions
 */
int libpdf_count_pages(fz_context *ctx, fz_document *doc)
{
    int pages;
    fz_try(ctx) {
        pages = fz_count_pages(ctx, doc);
    }
    fz_catch(ctx) {
        pages = -1;
    }
    return pages;
}

void libpdf_render_page(
    fz_context *ctx, fz_frame *frame,
    float ox, float oy,
    float xscale, float yscale,
    float rotation
) {
    fz_try(ctx) {
        fz_matrix transform = frame->transform;
        frame->error = 5;
        fz_pre_translate(&transform, ox / frame->xratio, oy / frame->yratio);
        frame->error = 6;
        fz_pre_rotate(&transform, rotation);
        frame->error = 7;
        fz_pre_scale(&transform, xscale, yscale);
        frame->error = 8;
        fz_clear_pixmap_with_value(ctx, frame->pix, 0xff);
        frame->error = 9;
        fz_run_page(ctx, frame->page, frame->dev, &transform, NULL);
        frame->error = 0;
    }
    fz_catch(ctx) {
    }
}
int libpdf_error_page(fz_context *ctx, fz_frame *frame)
{
    return frame->error;
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
